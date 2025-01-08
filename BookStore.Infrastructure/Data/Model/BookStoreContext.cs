using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookStore.domain;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


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
    {
        var config = new ConfigurationBuilder().AddUserSecrets<BookStoreContext>().Build();

        //var connectionstring = new SqlConnectionStringBuilder()
        //{
        //    ServerSPN = config["ServerName"],
        //    InitialCatalog = config["DataBaseName"],
        //    TrustServerCertificate = true,
        //    IntegratedSecurity = true
        //}.ToString();

        optionsBuilder.UseSqlServer("Initial Catalog=labb1_bokhandel;Integrated Security=True;Trust Server Certificate=True;Server SPN=localhost");
        
        //optionsBuilder.UseSqlServer(connectionstring);

        //var connectionstring = config["ConnectionString"];
        //optionsBuilder.UseSqlServer("connectionstring");
    }


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
