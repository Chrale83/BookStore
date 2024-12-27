using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Presentation.ViewModels
{
    internal class InventoryViewModel : ViewModelBase
    {
        private Store? _selectedStore;
        private ObservableCollection<StoreInventoryModel> _storeInventory;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged();
                
                LoadStoreStock();
                //OnPropertyChanged("StoreInventory");
            }
        }
                
                
        public InventoryViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, (r, message) =>
            {
                SelectedStore = message.SelectedStore;
            });
        }

        public ObservableCollection<StoreInventoryModel> StoreInventory
        {
            get => _storeInventory;
            set
            {
                _storeInventory = value;
                OnPropertyChanged();
            }
        }


        public void LoadStoreStock()
        {
            using var db = new BookStoreContext();

            var StoreStock = db.BookStoreInventories
                             .Include(bi => bi.Isbn13Navigation)
                             .ThenInclude(b => b.Authors)
                             .Include(bi => bi.Store)
                             .Where(bi => bi.StoreId == SelectedStore.Id)
                             .Select(bi => new StoreInventoryModel
                             {
                                 Isbn13 = bi.Isbn13Navigation.Isbn13,
                                 Title = bi.Isbn13Navigation.Title,
                                 Authors = bi.Isbn13Navigation.Authors.ToList(),
                                 StockCount = bi.StockCount
                             }).ToList();

            StoreInventory = new ObservableCollection<StoreInventoryModel>(StoreStock);
            

        }

        //public void LoadStoreStock()
        //{
        //    using var db = new BookStoreContext();

        //    var query = from b in db.Books
        //                join bi in db.BookStoreInventories on b.Isbn13 equals bi.Isbn13
        //                join s in db.Stores on bi.StoreId equals s.Id
        //                where s.Id == SelectedStore.Id // Använd SelectedStore.Id
        //                select new StoreInventoryModel
        //                {
        //                    Isbn13 = b.Isbn13,
        //                    Title = b.Title,
        //                    Authors = b.Authors.ToList(),
        //                    StockCount = bi.StockCount,
                            
        //                };

        //    var result = query.ToList();
            
        //    // Initialisera en ObservableCollection med resultatet
        //    StoreInventory = new ObservableCollection<StoreInventoryModel>(result);
        //}

        




    }
}
