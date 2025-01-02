using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class EditBooksHandler
    {

        public ObservableCollection<BookDataModel>? LoadBookTitles(Store selectedStore)
        {
            if (selectedStore != null)
            {
                using var db = new BookStoreContext();

                var GetBookData = db.BookStoreInventories
                                .AsNoTracking()
                                .Include(bi => bi.Isbn13Navigation)
                                .Where(bi => bi.StoreId == selectedStore.Id)
                                .Select(bi => new BookDataModel
                                {
                                    Isbn13 = bi.Isbn13Navigation.Isbn13,
                                    Title = bi.Isbn13Navigation.Title,
                                    StockCount = bi.StockCount
                                }).ToList();

                return new ObservableCollection<BookDataModel>(GetBookData);
            }
            return null;
        }

        public void UpDateBookStock(int selectedStore, string selectedBook, int bookStockCounter)
        {
            using var db = new BookStoreContext();

            var UpdateBookData = db.BookStoreInventories
                                .Include(bi => bi.Isbn13Navigation)
                                .FirstOrDefault(bi => bi.StoreId == selectedStore && bi.Isbn13Navigation.Isbn13 == selectedBook);

             bool editValue = CheckIfValidEdit(UpdateBookData.StockCount, bookStockCounter);
             
            if (editValue)
            {
                UpdateBookData.StockCount += bookStockCounter;
                db.SaveChanges();
            }
            else
            {
                //Öppna dialog window ErrorEditBookCount
            }

        }

        public static bool CheckIfValidEdit(int? dbStockAmount, int WantedValue)
        {
            int tempDbValue = (int)dbStockAmount;
            int tempBookEditValue = WantedValue;

            int tempTotalvalue = tempDbValue + tempBookEditValue;

            return tempTotalvalue > 0;
        }
    }
}