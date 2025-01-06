using BookStore.domain;
using BookStore.Presentation.Command;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.DialogWindows;
using BookStore.Presentation.Messages;
using BookStore.Presentation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace BookStore.Presentation.ViewModels
{
    internal class EditBookStockViewModel : ViewModelBase
    {
        private Store? _selectedStore;
        private ObservableCollection<BookDataModel>? _bookDatas;

        private int _bookStockCounter = 0;
        private string _addOrRemoveButtonText;
        public string _searchText;

        public string SearchText //<------- TEST
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();


            }
        }
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

        

        public int BookStockCounter
        {
            get => _bookStockCounter;
            set
            {
                _bookStockCounter = value;
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
        private void AddToBookCounter(object obj) => CheckEditedValueForBookStockCounter(1);
        private void SubtractFromBookCounter(object obj) => CheckEditedValueForBookStockCounter(-1);

        private void CheckEditedValueForBookStockCounter(int inputedValue)
        {
            if (inputedValue == 1)
            {
                BookStockCounter++;
            }
            else
            {
                BookStockCounter--;
            }
            CheckWhatTextForButton();
        }
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
            bool editSucess = await EditBooksHandler.UpdateBookStoreDataBaseStock(SelectedStore.Id, SelectedBook.Isbn13, BookStockCounter);

            if (editSucess) await UpdateBookDatas();
            else
            {
                var window = new ErrorEditBookCount();
                window.ShowDialog();
            }
            BookStockCounter = 0;
        }

        public async Task UpdateBookDatas()
        {
            BookDatas = await EditBooksHandler.LoadBookTitles(SelectedStore);
        }
        private void CheckWhatTextForButton()
        {
            //https://github.com/josephRashidMaalouf/AlternativeToIfStatementsDemo/blob/master/AlternativeToIfStatementsDemo/Program.cs //LÄnk till josefs guide över olika if och switch
            AddOrRemoveButtonText = BookStockCounter switch //beroende vilket av "Vilkoren" baserad på BookStockCounter´s värde så får AddOrRemoveButtonText "sträng värdet"
            {
                > 0 => "Add Book",
                < 0 => "Remove Book",
                _ => "Enter a number" // _ används som en else
            };
        }
    }
}



























