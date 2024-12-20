using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
       
            builder.HasKey(e => e.Id).HasName("PK__publishe__3214EC2775735D78");

            builder.ToTable("publisher");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phoneNumber");
            builder.Property(e => e.Website).HasColumnName("website");
        
    }
}
