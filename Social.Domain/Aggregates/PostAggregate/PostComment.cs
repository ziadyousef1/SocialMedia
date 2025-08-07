using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Aggregates.PostAggregate
{
    public class PostComment
    {
        private PostComment() { }
        
        public Guid PostCommentId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public string? Text { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public static PostComment Create(Guid postId, Guid userProfileId, string text)
        {
            return new PostComment
            {
                PostCommentId = Guid.NewGuid(),
                PostId = postId,
                UserProfileId = userProfileId,
                Text = text,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
        }
        public void UpdateText(string newText)
        {
            Text = newText;
            LastModifiedDate = DateTime.Now;
        }
    }
}
