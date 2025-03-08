using BlazorBlog.Server.Domain;
using BlazorBlog.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorBlog.Server.Persistence.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(MaxLengths.Blogs.Title);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(MaxLengths.Blogs.Description);

        builder.ToTable("Blogs");
    }
}