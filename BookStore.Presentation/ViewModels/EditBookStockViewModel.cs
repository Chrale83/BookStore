using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Command;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace BookStore.Presentation.ViewModels
{
    internal class EditBookStockViewModel : ViewModelBase
    {


        private Store? _selectedStore;
        private ObservableCollection<BookDataModel>? _bookDatas;

        public ObservableCollection<BookDataModel>? BookDatas
        {
            get => _bookDatas;
            set
            {
                _bookDatas = value;
                OnPropertyChanged(nameof(BookDatas));
            }
        }

        private BookDataModel? _selectedBook;

        public BookDataModel? SelectedBook
        {
            get => _selectedBook;
            set 
            { 
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        private int _bookStockCounter = 0;
        private string _addOrRemoveButtonText;

        public int BookStockCounter
        {
            get => _bookStockCounter;
            set 
            { 
                _bookStockCounter = value;
                if (BookStockCounter > 1)
                {
                    AddOrRemoveButtonText = "Add Book";
                }
                else
                {
                    AddOrRemoveButtonText = "Default";
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(AddOrRemoveButtonText));
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




        public RelayCommand AddTooBookCounterCommand { get; }

        private void AddToBookCounter(object obj)
        {
            
            BookStockCounter++;
        }

        public RelayCommand SubtractTooBookCounterCommand { get; }
        
        public string AddOrRemoveButtonText { 
            get => _addOrRemoveButtonText;
            set 
            { 
                _addOrRemoveButtonText = value;
                OnPropertyChanged();
            } 
        }

        public EditBookStockViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, (r, message) =>
            {
                SelectedStore = message.SelectedStore;
            });
            AddTooBookCounterCommand = new RelayCommand(AddToBookCounter);
            SubtractTooBookCounterCommand = new RelayCommand(SubtractFromBookCounter);
            
        }

        

        private void SubtractFromBookCounter(object obj)
        {
            BookStockCounter--;
            OnPropertyChanged(nameof(BookStockCounter));
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





