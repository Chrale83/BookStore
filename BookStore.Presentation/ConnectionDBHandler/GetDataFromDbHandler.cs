using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class GetDataFromDbHandler
    {

        public static async Task<ObservableCollection<Author>> GetAuthorsAsync()
        {
            using var db = new BookStoreContext();

            var GetAuthors = await db.Authors.ToListAsync();

            return new ObservableCollection<Author>(GetAuthors);
        }

        public static async Task<ObservableCollection<Publisher>> GetPublishersAsync()
        {
            using var db = new BookStoreContext();

            var getPublishers = await db.Publishers.ToListAsync();

            return new ObservableCollection<Publisher>(getPublishers);
        }

        public static async Task<ObservableCollection<Book>> GetAllBooksFromDbAsync(int selectedStore)
        {
            using var db = new BookStoreContext();

            var booksNotInStore = await db.Books
                                  .Where(b => !db.BookStoreInventories
                                  .Any(bsi => bsi.Isbn13 == b.Isbn13 && bsi.StoreId == selectedStore))
                                  .ToListAsync();
            return new ObservableCollection<Book>(booksNotInStore);
        }

        public static async Task<ObservableCollection<Book>> GetBooksInSelectedStoreAsync(int selectedStore)
        {
            using var db = new BookStoreContext();

            var booksFromStore = await db.Books
                                   .Where(b => db.BookStoreInventories
                                   .Any(bsi => bsi.Isbn13 == b.Isbn13 && bsi.StoreId == selectedStore))
                                   .ToListAsync();

            return new ObservableCollection<Book>(booksFromStore);
        }

        







    }
}


