using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Models;
using Social.Application.Enums;
using Social.Application.Posts.Queries;
using Social.DAL;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Post.QueryHandlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, OperationResult<PostEntity>>
    {
        private readonly DataContext _context;

        public GetPostByIdQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostEntity>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostEntity>();
            try
            {
                var post = await _context.Posts
                    .Include(p => p.UserProfile)
                    .Include(p => p.Comments)
                    .Include(p => p.Interactions)
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
