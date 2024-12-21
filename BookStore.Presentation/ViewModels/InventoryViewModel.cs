using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class InventoryViewModel : ViewModelBase
    {
        private Store? _selectedStore;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged();
                //LoadStoreInventory();
                
                OnPropertyChanged(nameof(BookStoreInventories));
                OnPropertyChanged(nameof(Books));
            }
        }

        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<BookStoreInventory> BookStoreInventories { get; set; } 

        //private void LoadStoreInventory()
        //{
        //    using var db = new BookStoreContext();
        //    BookStoreInventories = new ObservableCollection<BookStoreInventory>(
        //        db.BookStoreInventories.Where(i => i.StoreId == SelectedStore.Id). //PLOCKA UT STORECOUNTEN
            
        //}

        
    }
}
