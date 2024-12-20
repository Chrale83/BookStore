using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class BookStoreInventory
{
    public int StoreId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public int? StockCount { get; set; }

    public virtual Book Isbn13Navigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
