using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        
            builder.ToTable("genre");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Name).HasColumnName("name");
        
    }
}
