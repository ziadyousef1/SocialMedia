using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.DAL.Configurations
{
    internal class PostCommentConfig:IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(x => x.PostCommentId);
            builder.Property(x => x.PostId).IsRequired();
            builder.Property(x => x.UserProfileId).IsRequired();
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.LastModifiedDate).IsRequired();
        }
    }   
 
}
