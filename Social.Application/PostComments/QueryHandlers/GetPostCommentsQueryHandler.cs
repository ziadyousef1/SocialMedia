using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.PostComments.Queries;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.QueryHandlers
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, OperationResult<IEnumerable<PostComment>>>
    {
        private readonly DataContext _context;

        public GetPostCommentsQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<IEnumerable<PostComment>>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEnumerable<PostComment>>();
            try
            {
                var comments = await _context.PostComments
                    .Where(c => c.PostId == request.PostId)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync(cancellationToken);

                operationResult.Payload = comments;
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
