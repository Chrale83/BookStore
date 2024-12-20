using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class BookStoreInventoryEntityTypeConfiguration : IEntityTypeConfiguration<BookStoreInventory>
{
    public void Configure(EntityTypeBuilder<BookStoreInventory> builder)
    {
        
            builder.HasKey(e => new { e.StoreId, e.Isbn13 }).HasName("PK__bookStor__D40710556B8FFB9D");

            builder.ToTable("bookStoreInventory");

            builder.Property(e => e.StoreId).HasColumnName("storeID");
            builder.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isbn13");
            builder.Property(e => e.StockCount).HasColumnName("stockCount");

            builder.HasOne(d => d.Isbn13Navigation).WithMany(p => p.BookStoreInventories)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookStore__isbn1__76969D2E");

            builder.HasOne(d => d.Store).WithMany(p => p.BookStoreInventories)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreID_Stores");
        
    }
}
