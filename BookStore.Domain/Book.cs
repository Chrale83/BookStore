using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public DateOnly? DateReleased { get; set; }

    public int? PublisherId { get; set; }

    public int? Pages { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<BookStoreInventory> BookStoreInventories { get; set; } = new List<BookStoreInventory>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
