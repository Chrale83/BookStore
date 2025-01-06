using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.DialogWindows;
using BookStore.Presentation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class EditBooksHandler
    {
        public static async Task<ObservableCollection<BookDataModel>>? LoadBookTitles(Store selectedStore)
        {
            try
            {
                if (selectedStore == null)
                {
                    return null;
                }



                using var db = new BookStoreContext();

                var GetBookData = await db.BookStoreInventories
                                .AsNoTracking()
                                .Include(bi => bi.Isbn13Navigation)
                                .Where(bi => bi.StoreId == selectedStore.Id)
                                .Select(bi => new BookDataModel
                                {
                                    Isbn13 = bi.Isbn13Navigation.Isbn13,
                                    Title = bi.Isbn13Navigation.Title,
                                    StockCount = bi.StockCount
                                }).ToListAsync();

                return new ObservableCollection<BookDataModel>(GetBookData);
            }
            catch
            {

                return null;
            }
        }

        public static async Task<bool> UpdateBookStoreDataBaseStock(int selectedStore, string selectedBook, int bookStockCounter)
        {
            try
            {
                using var db = new BookStoreContext();

                var UpdateBookData = db.BookStoreInventories
                                    .Include(bi => bi.Isbn13Navigation)
                                    .FirstOrDefault(bi => bi.StoreId == selectedStore && bi.Isbn13Navigation.Isbn13 == selectedBook);

                bool editValue = CheckIfValidEdit(UpdateBookData.StockCount, bookStockCounter);

                if (editValue)
                {
                    UpdateBookData.StockCount += bookStockCounter;
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                var ErrorWindow = new ErrorNoConnectionToDataBase();
                ErrorWindow.ShowDialog();
                return false;
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



//public async Task<bool> UpdateBookStoreDataBaseStock(int selectedStore, string selectedBook, int bookStockCounter)
//{
//    try
//    {
//        using var db = new BookStoreContext();

//        var UpdateBookData = db.BookStoreInventories
//                            .Include(bi => bi.Isbn13Navigation)
//                            .FirstOrDefault(bi => bi.StoreId == selectedStore && bi.Isbn13Navigation.Isbn13 == selectedBook);

//        bool editValue = CheckIfValidEdit(UpdateBookData.StockCount, bookStockCounter);

//        if (editValue)
//        {
//            UpdateBookData.StockCount += bookStockCounter;
//            await db.SaveChangesAsync();
//        }
//        else
//        {
//            throw new Exception("Bla");
//        }
//    }
//            //catch(e)
//            //{
//            //    var ErrorWindow = new ErrorNoConnectionToDataBase();
//            //    ErrorWindow.ShowDialog();
//            //    throw e;
//            //}
//        }