using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.PostInteractions.Queries;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostInteractions.QueryHandlers
{
    public class GetPostInteractionsQueryHandler : IRequestHandler<GetPostInteractionsQuery, OperationResult<IEnumerable<PostInteraction>>>
    {
        private readonly DataContext _context;

        public GetPostInteractionsQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<IEnumerable<PostInteraction>>> Handle(GetPostInteractionsQuery request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEnumerable<PostInteraction>>();
            try
            {
                var interactions = await _context.PostInteractions
                    .Where(pi => pi.PostId == request.PostId)
                    .OrderByDescending(pi => pi.InteractionDate)
                    .ToListAsync(cancellationToken);

                operationResult.Payload = interactions;
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
