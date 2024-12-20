using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class Publisher
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
