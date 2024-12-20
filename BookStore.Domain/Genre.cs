using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class Genre
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
