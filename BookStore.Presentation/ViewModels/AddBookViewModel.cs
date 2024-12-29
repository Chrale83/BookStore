using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class AddBookViewModel : ViewModelBase
    {


        private Store? _selectedStore;
        private ObservableCollection<BookDataModel> _bookDatas;

        public ObservableCollection<BookDataModel> BookDatas
        {
            get => _bookDatas;
            set
            {
                _bookDatas = value;
                OnPropertyChanged(nameof(BookDatas));
            }
        }



        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                if (SelectedStore != value)
                {
                    _selectedStore = value;
                    LoadBookTitles();
                }



                OnPropertyChanged();


            }
        }

        public AddBookViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, (r, message) =>
            {
                SelectedStore = message.SelectedStore;
            });
        }

        public void LoadBookTitles()
        {
            if (SelectedStore != null)
            {
                using var db = new BookStoreContext();

                var GetBookData = db.BookStoreInventories
                                .Include(bi => bi.Isbn13Navigation)
                                .Where(bi => bi.StoreId == SelectedStore.Id)
                                .Select(bi => new BookDataModel
                                {
                                    Isbn13 = bi.Isbn13Navigation.Isbn13,
                                    Title = bi.Isbn13Navigation.Title,
                                    StockCount = bi.StockCount
                                }).ToList();

                BookDatas = new ObservableCollection<BookDataModel>(GetBookData);

            }
        }

    }
}
