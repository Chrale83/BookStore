using System;
using System.Collections.Generic;

namespace BookStore.domain;

public partial class TitlesPerAuthor
{
    public string Name { get; set; } = null!;

    public string Age { get; set; } = null!;

    public int? TotalNumberOfTitles { get; set; }

    public decimal? TotalValueKr { get; set; }

    public string? Status { get; set; }
}
