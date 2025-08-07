using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Posts.Commands;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.CommandHandlers
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, OperationResult<PostEntity>>
    {
        private readonly DataContext _context;

        public DeletePostCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostEntity>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostEntity>();
            try
            {
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

                _context.Posts.Remove(post);
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
