using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class TitlesPerAuthorEntityTypeConfiguration : IEntityTypeConfiguration<TitlesPerAuthor>
{
    public void Configure(EntityTypeBuilder<TitlesPerAuthor> builder)
    {
        
            builder
                .HasNoKey()
                .ToView("titlesPerAuthor");

            builder.Property(e => e.Age)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("age");
            builder.Property(e => e.Name).HasMaxLength(51);
            builder.Property(e => e.Status).HasMaxLength(5);
            builder.Property(e => e.TotalNumberOfTitles).HasColumnName("Total number of titles");
            builder.Property(e => e.TotalValueKr)
                .HasColumnType("money")
                .HasColumnName("Total value (kr)");
        
    }
}