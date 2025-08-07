using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Posts.Commands;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.CommandHandlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, OperationResult<PostEntity>>
    {
        private readonly DataContext _context;

        public CreatePostCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostEntity>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostEntity>();
            try
            {
                // Verify user profile exists
                var userProfileExists = await _context.UserProfiles
                    .AnyAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

                if (!userProfileExists)
                {
                    operationResult.IsSuccess = false;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"User profile with id {request.UserProfileId} not found."
                    };
                    operationResult.Errors.Add(error);
                    return operationResult;
                }

                var post = PostEntity.Create(request.UserProfileId, request.TextContent, request.PhotoUrl, request.PhotoFileName);
                
                _context.Posts.Add(post);
                await _context.SaveChangesAsync(cancellationToken);
                operationResult.Payload = post;
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
