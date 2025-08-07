using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostInteractions.Commands
{
    public class AddPostInteractionCommand : IRequest<OperationResult<PostInteraction>>
    {
        public required Guid PostId { get; set; }
        public required Guid UserProfileId { get; set; }
        public required InteractionType InteractionType { get; set; }
    }
}
