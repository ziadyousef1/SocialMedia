using MediatR;
using Social.Application.Models;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.Commands
{
    public class DeletePostCommand : IRequest<OperationResult<PostEntity>>
    {
        public required Guid PostId { get; set; }
    }
}
