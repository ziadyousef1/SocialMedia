using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.PostInteractions.Commands;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostInteractions.CommandHandlers
{
    public class RemovePostInteractionCommandHandler : IRequestHandler<RemovePostInteractionCommand, OperationResult<PostInteraction>>
    {
        private readonly DataContext _context;

        public RemovePostInteractionCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostInteraction>> Handle(RemovePostInteractionCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostInteraction>();
            try
            {
                var interaction = await _context.PostInteractions
                    .FirstOrDefaultAsync(pi => pi.PostId == request.PostId && pi.UserProfileId == request.UserProfileId, cancellationToken);

                if (interaction == null)
                {
                    operationResult.IsSuccess = false;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No interaction found for user {request.UserProfileId} on post {request.PostId}."
                    };
                    operationResult.Errors.Add(error);
                    return operationResult;
                }

                _context.PostInteractions.Remove(interaction);
                await _context.SaveChangesAsync(cancellationToken);
                operationResult.Payload = interaction;
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
