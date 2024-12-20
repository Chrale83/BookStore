using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class StoreAuthorEntityTypeConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        
            builder.HasKey(e => e.Id).HasName("PK__shops__3214EC274F5625D6");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.City).HasColumnName("city");
            builder.Property(e => e.Country).HasColumnName("country");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Postnr)
                .HasMaxLength(5)
                .HasColumnName("postnr");
            builder.Property(e => e.Street).HasColumnName("street");
            builder.Property(e => e.StreetNr)
                .HasMaxLength(3)
                .HasColumnName("streetNr");
        
    }
}
