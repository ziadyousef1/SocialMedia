using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostInteractions.Commands
{
    public class RemovePostInteractionCommand : IRequest<OperationResult<PostInteraction>>
    {
        public required Guid PostId { get; set; }
        public required Guid UserProfileId { get; set; }
    }
}
