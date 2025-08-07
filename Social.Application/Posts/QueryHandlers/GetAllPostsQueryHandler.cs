using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Posts.Queries;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.QueryHandlers
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, OperationResult<IEnumerable<PostEntity>>>
    {
        private readonly DataContext _context;

        public GetAllPostsQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<IEnumerable<PostEntity>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEnumerable<PostEntity>>();
            try
            {
                var posts = await _context.Posts
                    .Include(p => p.UserProfile)
                    .Include(p => p.Comments)
                    .Include(p => p.Interactions)
                    .OrderByDescending(p => p.CreatedDate)
                    .ToListAsync(cancellationToken);

                operationResult.Payload = posts;
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
