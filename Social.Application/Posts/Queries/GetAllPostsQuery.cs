using MediatR;
using Social.Application.Models;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.Queries
{
    public class GetAllPostsQuery : IRequest<OperationResult<IEnumerable<PostEntity>>>
    {
    }
}
