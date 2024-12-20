using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class Author
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateOnly? DateOfDeath { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
