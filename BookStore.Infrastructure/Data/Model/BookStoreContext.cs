using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookStore.domain;
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
