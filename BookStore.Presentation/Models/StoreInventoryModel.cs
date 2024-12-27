using BookStore.domain;
using System.Collections.ObjectModel;


namespace BookStore.Presentation.Models
{
    internal class StoreInventoryModel
    {

        public string? Isbn13 { get; set; }
        public string? Title { get; set; }
        
        public int? StockCount { get; set; }
        
        public ICollection<Author> Authors { get; internal set; }

        public string AuthorsNames => string.Join(", ", Authors.Select(a => $"{a.Firstname} {a.Lastname}"));
    }
}
