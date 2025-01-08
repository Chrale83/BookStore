using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.DialogWindows;
using BookStore.Presentation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class LoadStoreInventoryHandler
    {

        public static async Task<ObservableCollection<StoreInventoryModel>> LoadStoreStockAsync(int? selectedStore)
        {
            try
            {
                if (selectedStore.HasValue)
                {

                    using var db = new BookStoreContext();

                    var StoreStock = await db.BookStoreInventories
                                     .Include(bi => bi.Isbn13Navigation)
                                     .ThenInclude(b => b.Authors)
                                     .Include(bi => bi.Store)
                                     .Where(bi => bi.StoreId == selectedStore)
                                     .Select(bi => new StoreInventoryModel
                                     {
                                         Isbn13 = bi.Isbn13Navigation.Isbn13,
                                         Title = bi.Isbn13Navigation.Title,
                                         Authors = bi.Isbn13Navigation.Authors.ToList(),
                                         StockCount = bi.StockCount
                                     }).ToListAsync();

                    return new ObservableCollection<StoreInventoryModel>(StoreStock);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                var ErrorWindow = new ErrorNoConnectionToDataBase();
                ErrorWindow.ShowDialog();
                return null;
            }
        }

    }
}

