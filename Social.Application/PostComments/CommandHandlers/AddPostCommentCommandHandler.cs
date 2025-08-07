using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.PostComments.Commands;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.CommandHandlers
{
    public class AddPostCommentCommandHandler : IRequestHandler<AddPostCommentCommand, OperationResult<PostComment>>
    {
        private readonly DataContext _context;

        public AddPostCommentCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostComment>> Handle(AddPostCommentCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostComment>();
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

                // Create new comment
                var newComment = PostComment.Create(request.PostId, request.UserProfileId, request.Text);
                
                _context.PostComments.Add(newComment);
                await _context.SaveChangesAsync(cancellationToken);
                operationResult.Payload = newComment;
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
