using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        
            builder.HasKey(e => e.Isbn13).HasName("PK__books__AA00666DDFE44230");

            builder.ToTable("books");

            builder.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isbn13");
            builder.Property(e => e.DateReleased).HasColumnName("dateReleased");
            builder.Property(e => e.Language).HasColumnName("language");
            builder.Property(e => e.Pages).HasColumnName("pages");
            builder.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            builder.Property(e => e.PublisherId).HasColumnName("publisherID");
            builder.Property(e => e.Title).HasColumnName("title");

            builder.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK_publisher");

            builder.HasMany(d => d.Genres).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_junction_genre"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_junction_book_genre"),
                    j =>
                    {
                        j.HasKey("Isbn13", "GenreId").HasName("PK__book_gen__B820E64716292072");
                        j.ToTable("book_genre");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("isbn13");
                        j.IndexerProperty<int>("GenreId").HasColumnName("genreID");
                    });
        
    }
}
