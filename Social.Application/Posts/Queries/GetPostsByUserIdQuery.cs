using MediatR;
using Social.Application.Models;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.Queries
{
    public class GetPostsByUserIdQuery : IRequest<OperationResult<IEnumerable<PostEntity>>>
    {
        public required Guid UserProfileId { get; set; }
    }
}
