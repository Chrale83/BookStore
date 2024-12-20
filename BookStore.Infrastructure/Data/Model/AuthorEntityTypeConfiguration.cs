using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Infrastructure.Data.Model;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        
            builder.HasKey(e => e.Id).HasName("PK__Author__3213E83F9F921E7A");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            builder.Property(e => e.DateOfDeath).HasColumnName("dateOfDeath");
            builder.Property(e => e.Firstname)
                .HasMaxLength(25)
                .HasColumnName("firstname");
            builder.Property(e => e.Lastname)
                .HasMaxLength(25)
                .HasColumnName("lastname");
            builder.Property(e => e.Status).HasMaxLength(5);

            builder.HasMany(d => d.Isbn13s).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorBook",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_junction_book_author"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Junction_author_book"),
                    j =>
                    {
                        j.HasKey("AuthorId", "Isbn13").HasName("PK__author_b__448737BF283120B3");
                        j.ToTable("author_books");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("authorID");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("isbn13");
                    });
        
    }
}
