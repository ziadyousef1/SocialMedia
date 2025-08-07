using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostInteractions.Queries
{
    public class GetPostInteractionsQuery : IRequest<OperationResult<IEnumerable<PostInteraction>>>
    {
        public required Guid PostId { get; set; }
    }
}
