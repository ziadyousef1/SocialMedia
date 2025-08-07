using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.Queries
{
    public class GetPostCommentByIdQuery : IRequest<OperationResult<PostComment>>
    {
        public required Guid PostCommentId { get; set; }
    }
}
