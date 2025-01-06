using BookStore.domain;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using BookStore.Presentation.Messages;
using BookStore.Presentation.ConnectionDBHandler;

namespace BookStore.Presentation.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private Store _selectedStore;
        

        public ObservableCollection<Store> Stores { get; set; }

        public Store SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged();
                WeakReferenceMessenger.Default.Send(new SelectedStoreMessage(_selectedStore));
            }
        }
        public MenuViewModel()
        {
            LoadStoresFromDb();
        }
        private async void LoadStoresFromDb()
        {
            Stores = await LoadStoreHandler.LoadStores();
            OnPropertyChanged(nameof(Stores));
            SelectedStore = Stores.FirstOrDefault();
        }
    }
}

