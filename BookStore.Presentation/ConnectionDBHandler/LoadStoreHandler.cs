using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class LoadStoreHandler
    {
        public ObservableCollection<Store> LoadStores()
        {
            using var db = new BookStoreContext();

            return new ObservableCollection<Store>(db.Stores.ToList());
        }
    }
}
