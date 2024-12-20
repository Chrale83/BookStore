using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;


namespace BookStore.Infrastructure.Data.Model;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookStoreInventory> BookStoreInventories { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Initial Catalog=labb1_bokhandel;Integrated Security=True;Trust Server Certificate=True;Server SPN=localhost");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("finnish_swedish_CI_AS");

        new AuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Author>());
        new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
        new BookStoreInventoryEntityTypeConfiguration().Configure(modelBuilder.Entity<BookStoreInventory>());
        new GenreEntityTypeConfiguration().Configure(modelBuilder.Entity<Genre>());
        new PublisherEntityTypeConfiguration().Configure(modelBuilder.Entity<Publisher>());
        new TitlesPerAuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<TitlesPerAuthor>());
        new StoreAuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Store>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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

public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        
            builder.ToTable("genre");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Name).HasColumnName("name");
        
    }
}

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