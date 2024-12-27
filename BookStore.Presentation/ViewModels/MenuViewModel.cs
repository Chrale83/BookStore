using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using BookStore.Presentation.Messages;

namespace BookStore.Presentation.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private Store _selectedStore;
        

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
