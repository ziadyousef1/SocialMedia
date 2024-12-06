using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Aggregates.PostAggregate
{
    public class PostInteraction
    {
        private PostInteraction()
        {
        }
        public Guid PostInteractionId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public DateTime InteractionDate { get; private set; }
        public InteractionType InteractionType { get; private set; }

        public static PostInteraction Create(Guid postId, Guid userProfileId, InteractionType interactionType)
        {
            return new PostInteraction
            {
                PostId = postId,
                UserProfileId = userProfileId,
                InteractionType = interactionType,
                InteractionDate = DateTime.Now
            };
        }
    }
}
