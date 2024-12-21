using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private Store _selectedStore;
        //Denna klass ska hantera logiken som ligger MenuView.
        //- Hanterar att det står rätt butiksnamn i topmainview
        //-Inladdning av "Stores" **
        //-Byte till "InventoryView"
        //-Byte till "add book" view
        //-Avsluta programmet

        public ObservableCollection<Store> Stores { get; set; }
        public Store SelectedStore {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel()
        {
            LoadStores();
        }

        public void LoadStores()
        {
            using var db = new BookStoreContext();

            Stores = new ObservableCollection<Store>(db.Stores.ToList());
        }
    }


}
