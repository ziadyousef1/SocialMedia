using Social.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        private  Post()
        {
            Comments = new List<PostComment>();
            Interactions = new List<PostInteraction>();

        }
        public Guid PostId { get; private set; }
        public Guid UserProfileID { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string TextContent { get; private set; }
        public string? PhotoUrl { get; private set; }
        public string? PhotoFileName { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public List<PostComment> Comments { get; private set; }
        public List<PostInteraction> Interactions { get; private set; }
        public static Post Create(Guid userProfileId, string textContent, string? photoUrl = null, string? photoFileName = null)
        {
            return new Post
            {
                PostId = Guid.NewGuid(),
                UserProfileID = userProfileId,
                TextContent = textContent,
                PhotoUrl = photoUrl,
                PhotoFileName = photoFileName,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
        }
        public void UpdateTextContent(string newText)
        {
            TextContent = newText;
            LastModifiedDate = DateTime.Now;
        }
        
        public void UpdatePhoto(string? photoUrl, string? photoFileName)
        {
            PhotoUrl = photoUrl;
            PhotoFileName = photoFileName;
            LastModifiedDate = DateTime.Now;
        }
        
        public void RemovePhoto()
        {
            PhotoUrl = null;
            PhotoFileName = null;
            LastModifiedDate = DateTime.Now;
        }
        public void AddComment(PostComment comment)
        {
            Comments.Add(comment);
        }
        public void RemoveComment(PostComment comment)
        {
            Comments.Remove(comment);
        }

        public void AddInteraction(PostInteraction interaction)
        {
            Interactions.Add(interaction);
        }
        public void RemoveInteraction(PostInteraction interaction)
        {
            Interactions.Remove(interaction);
        }

    }
}
