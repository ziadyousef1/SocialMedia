using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.Commands
{
    public class DeletePostCommentCommand : IRequest<OperationResult<PostComment>>
    {
        public required Guid PostCommentId { get; set; }
    }
}
