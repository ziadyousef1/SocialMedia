using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.Queries
{
    public class GetPostCommentsQuery : IRequest<OperationResult<IEnumerable<PostComment>>>
    {
        public required Guid PostId { get; set; }
    }
}
