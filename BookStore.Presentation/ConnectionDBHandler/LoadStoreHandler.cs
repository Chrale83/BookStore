using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ConnectionDBHandler
{
    class LoadStoreHandler
    {
        public async Task<ObservableCollection<Store>> LoadStores()
        {
            try
            {

            using var db = new BookStoreContext();

            var Stores = await db.Stores.ToListAsync();

            return new ObservableCollection<Store>(Stores);
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
