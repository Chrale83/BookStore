using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class GetDataFromDbHandler
    {

        public static async Task<ObservableCollection<Author>> GetAuthorsAsync()
        {
            var db = new BookStoreContext();

            var GetAuthors = await db.Authors.ToListAsync();

            return new ObservableCollection<Author>(GetAuthors);
        }

        public static async Task<ObservableCollection<Publisher>> GetPublishersAsync()
        {
            var db = new BookStoreContext();

            var getPublishers = await db.Publishers.ToListAsync();

            return new ObservableCollection<Publisher>(getPublishers);
        }

    }
}

            
