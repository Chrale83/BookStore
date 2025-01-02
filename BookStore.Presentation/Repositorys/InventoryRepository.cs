using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Presentation.Repositorys
{
    class InventoryRepository
    {
        private readonly BookStoreContext _context;

        public InventoryRepository(BookStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<StoreInventoryModel> GetStoreStock(int storeId)
        {
            return _context.BookStoreInventories
                           .Include(bi => bi.Isbn13Navigation)
                           .ThenInclude(b => b.Authors)
                           .Include(bi => bi.Store)
                           .Where(bi => bi.StoreId == storeId)
                           .Select(bi => new StoreInventoryModel
                           {
                               Isbn13 = bi.Isbn13Navigation.Isbn13,
                               Title = bi.Isbn13Navigation.Title,
                               Authors = bi.Isbn13Navigation.Authors.ToList(),
                               StockCount = bi.StockCount
                           })
                           .ToList();
        }


    }
}
