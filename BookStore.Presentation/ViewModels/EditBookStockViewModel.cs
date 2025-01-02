using BookStore.domain;
using BookStore.Presentation.Command;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class EditBookStockViewModel : ViewModelBase
    {
        private Store? _selectedStore;
        private ObservableCollection<BookDataModel>? _bookDatas;
        private int _bookStockCounter = 0;
        private string _addOrRemoveButtonText;

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
                UpdateBookStockCommand.RaiseCanExectueChanged();
            }
        }

        public EditBooksHandler EditBooksHandler { get; set; } = new EditBooksHandler();

        public int BookStockCounter
        {
            get => _bookStockCounter;
            set
            {
                _bookStockCounter = value;
                if (BookStockCounter > 0)
                {
                    AddOrRemoveButtonText = "Add Book";
                }
                else if (BookStockCounter < 0)
                {
                    AddOrRemoveButtonText = "Remove Book";
                }
                else
                {
                    AddOrRemoveButtonText = "Enter a number";
                }
                OnPropertyChanged();
                UpdateBookStockCommand.RaiseCanExectueChanged();
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
                    UpdateBookDatas();
                }
                OnPropertyChanged();
                UpdateBookStockCommand.RaiseCanExectueChanged();
            }
        }
        private void AddToBookCounter(object obj) => BookStockCounter++;
        private void SubtractFromBookCounter(object obj) => BookStockCounter--;
        
        public string AddOrRemoveButtonText
        {
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
            StartupRelayCommands();
        }
        public void StartupRelayCommands()
        {
            AddTooBookCounterCommand = new RelayCommand(AddToBookCounter);
            SubtractTooBookCounterCommand = new RelayCommand(SubtractFromBookCounter);
            UpdateBookStockCommand = new RelayCommand(UpdateBookStock, CanUpdateBookStock);
        }
        public RelayCommand AddTooBookCounterCommand { get; set; }
        public RelayCommand SubtractTooBookCounterCommand { get; set; }
        public RelayCommand UpdateBookStockCommand { get; set; }


        private bool CanUpdateBookStock(object? arg)
        {
            bool isStoreSelected = SelectedStore != null;
            bool isBookSelected = SelectedBook != null;
            bool isBookStockAValue = BookStockCounter != 0;

            return isBookSelected && isStoreSelected && isBookStockAValue;
        }

        private void UpdateBookStock(object obj)
        {
            EditBooksHandler.UpDateBookStock(SelectedStore.Id, SelectedBook.Isbn13,BookStockCounter);
            UpdateBookDatas();
        }

        public void UpdateBookDatas()
        {
            BookDatas = EditBooksHandler.LoadBookTitles(SelectedStore);
        }
    }

}
                    

        







