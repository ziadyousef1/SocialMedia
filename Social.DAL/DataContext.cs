using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social.DAL.Configurations;
using Social.Domain.Aggregates.PostAggregate;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.DAL
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostInteraction> PostInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.ApplyConfiguration(new PostCommentConfig());
           builder.ApplyConfiguration(new PostInteractionConfig());
           builder.ApplyConfiguration(new UserProfileConfig());
           builder.ApplyConfiguration(new IdentityUserLoginConfig());
           builder.ApplyConfiguration(new IdentityUserRoleConfig());
           builder.ApplyConfiguration(new IdentityUserTokenConfig());


        }
    }
}
