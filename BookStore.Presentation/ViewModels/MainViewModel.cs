using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Store> Stores { get; private set; }

        private Store? _selectedStore;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                inventoryViewModel.SelectedStore = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel MenuViewModel { get; }
        public InventoryViewModel inventoryViewModel { get; }



        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel(this);
            inventoryViewModel = new InventoryViewModel();
            LoadStores();
        }

        public void LoadStores()
        {
            using var db = new BookStoreContext();

            Stores = new ObservableCollection<Store>(db.Stores.ToList());
            
                
        }
    }
}
