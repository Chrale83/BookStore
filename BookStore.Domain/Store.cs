using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class Store
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Street { get; set; }

    public string? StreetNr { get; set; }

    public string? Postnr { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<BookStoreInventory> BookStoreInventories { get; set; } = new List<BookStoreInventory>();
}
