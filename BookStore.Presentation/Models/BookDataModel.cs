using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Presentation.Models
{
    internal class BookDataModel
    {
        public string? Isbn13 { get; set; }
        public string? Title { get; set; }
        public int? StockCount { get; set; }
    }
}
