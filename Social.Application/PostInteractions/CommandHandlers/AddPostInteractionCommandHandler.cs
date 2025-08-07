using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.PostInteractions.Commands;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostInteractions.CommandHandlers
{
    public class AddPostInteractionCommandHandler : IRequestHandler<AddPostInteractionCommand, OperationResult<PostInteraction>>
    {
        private readonly DataContext _context;

        public AddPostInteractionCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostInteraction>> Handle(AddPostInteractionCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostInteraction>();
            try
            {
                // Check if post exists
                var post = await _context.Posts
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post == null)
                {
                    operationResult.IsSuccess = false;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"Post with id {request.PostId} not found."
                    };
                    operationResult.Errors.Add(error);
                    return operationResult;
                }

                // Check if user already has an interaction on this post
                var existingInteraction = await _context.PostInteractions
                    .FirstOrDefaultAsync(pi => pi.PostId == request.PostId && pi.UserProfileId == request.UserProfileId, cancellationToken);

                if (existingInteraction != null)
                {
                    // Remove existing interaction and create new one (simulating update)
                    _context.PostInteractions.Remove(existingInteraction);
                }

                // Create new interaction
                var newInteraction = PostInteraction.Create(request.PostId, request.UserProfileId, request.InteractionType);
                
                _context.PostInteractions.Add(newInteraction);
                await _context.SaveChangesAsync(cancellationToken);
                operationResult.Payload = newInteraction;
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Code = ErrorCode.ServerError,
                    Message = e.Message
                };
                operationResult.IsSuccess = false;
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }
    }
}
