using BookStore.domain;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using BookStore.Presentation.Messages;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.Command;

namespace BookStore.Presentation.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private Store _selectedStore;
        public LoadStoreHandler LoadStoreHandler { get; set; } = new LoadStoreHandler();

        public ObservableCollection<Store> Stores { get; set; }
        public Store SelectedStore {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged();
                WeakReferenceMessenger.Default.Send(new SelectedStoreMessage(_selectedStore));
                
            }
        }

        public RelayCommand GetStoresCommand { get; }
        public MenuViewModel()
        {
            GetStoresCommand = new RelayCommand(LoadStoresFromDb, CanLoadStoresFromDb);
            
        }

        private bool CanLoadStoresFromDb(object? arg)
        {
            return SelectedStore == null;
        }

        private async void LoadStoresFromDb(object obj)
        {
            Stores = await LoadStoreHandler.LoadStores();
            OnPropertyChanged(nameof(Stores));
            SelectedStore = Stores.FirstOrDefault();
            GetStoresCommand.RaiseCanExectueChanged();
        }
    }
}




