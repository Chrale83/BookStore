﻿using BookStore.domain;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class InventoryViewModel : ViewModelBase
    {
        public LoadStoreInventoryHandler LoadStoreInventoryHandler { get; set; } = new LoadStoreInventoryHandler();
        private Store? _selectedStore;
        private ObservableCollection<StoreInventoryModel>? _storeInventory;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
                LoadStoreStock();
            }
        }

        public InventoryViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, (r, message) => SelectedStore = message.SelectedStore);
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
