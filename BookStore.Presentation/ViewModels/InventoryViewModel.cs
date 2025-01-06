using BookStore.domain;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class InventoryViewModel : ViewModelBase
    {
        
        private Store? _selectedStore;
        private ObservableCollection<StoreInventoryModel>? _storeInventory;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }
                

        public InventoryViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, async (r, message) =>
            {
                SelectedStore = message.SelectedStore;
                await LoadStoreStock();
            });
        }

        public ObservableCollection<StoreInventoryModel>? StoreInventory
        {
            get => _storeInventory;
            set
            {
                _storeInventory = value;
                OnPropertyChanged();
            }
        }
        public async Task LoadStoreStock()
        {
            StoreInventory = await LoadStoreInventoryHandler.LoadStoreStockAsync(SelectedStore.Id);
        }
    }

}
