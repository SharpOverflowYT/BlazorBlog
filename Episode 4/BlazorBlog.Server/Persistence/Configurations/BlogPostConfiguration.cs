using BlazorBlog.Server.Domain;
using BlazorBlog.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorBlog.Server.Persistence.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(MaxLengths.BlogPosts.Title);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(MaxLengths.BlogPosts.Content);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasOne(x => x.Blog)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.BlogId)
            .IsRequired();

        builder.ToTable("BlogPosts");
    }
}