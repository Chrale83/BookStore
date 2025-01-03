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
                CheckWhatTextForButton();
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
            UpdateBookStockCommand = new RelayCommand(UpdateBookStoreStock, CanUpdateBookStock);
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

        private async void UpdateBookStoreStock(object obj)
        {
            await EditBooksHandler.UpdateBookStoreDataBaseStock(SelectedStore.Id, SelectedBook.Isbn13,BookStockCounter);
            await UpdateBookDatas();
            BookStockCounter = 0;
        }

        public async Task UpdateBookDatas()
        {
            BookDatas = await EditBooksHandler.LoadBookTitles(SelectedStore);
        }
        private void CheckWhatTextForButton()
        {
            //https://github.com/josephRashidMaalouf/AlternativeToIfStatementsDemo/blob/master/AlternativeToIfStatementsDemo/Program.cs
            AddOrRemoveButtonText = BookStockCounter switch
            {
                > 0 => "Add Book",
                < 0 => "Remove Book",
                _ => "Enter a number" // _ används som en else?

            };
        
        }
    }

}
             
            
           
                    

        







