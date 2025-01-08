using BookStore.domain;
using BookStore.Presentation.Command;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class RemoveBookFromStoreViewModel : ViewModelBase
    {

        private Store _selectedStore;

        public Store SelectedStore
        {
            get => _selectedStore;
            set 
            {
                _selectedStore = value;
                GetStoreInventory();
                OnPropertyChanged();
                RemoveBookCommand.RaiseCanExectueChanged();
            }
        }

        private ObservableCollection<Book> _booksInStore;

        public ObservableCollection<Book> BooksInStore
        {
            get => _booksInStore;
            set 
            { 
                _booksInStore = value;
                OnPropertyChanged();
            }
        }

        private Book _selectedBook;

        public Book  SelectedBook
        {
            get => _selectedBook; 
            set 
            { 
                _selectedBook = value;
                OnPropertyChanged();
                RemoveBookCommand.RaiseCanExectueChanged();
            }
        }
        public RelayCommand RemoveBookCommand { get; }

        public RemoveBookFromStoreViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, async (r, message) =>
            {
                SelectedStore = message.SelectedStore;
            });
            RemoveBookCommand = new RelayCommand(RemoveBookFromStore, CanRemoveBookFromStore);
        }

        private bool CanRemoveBookFromStore(object? arg)
        {
            return SelectedBook != null && SelectedStore != null;
        }

        public async void GetStoreInventory()
        {
            if (SelectedStore != null)
            {
                BooksInStore = await GetDataFromDbHandler.GetBooksInSelectedStoreAsync(SelectedStore.Id);
            }
        }

        public async void RemoveBookFromStore(object? arg)
        {
            await SetDataToDbHandler.RemoveBookFromStoreInventory(SelectedStore.Id, SelectedBook.Isbn13);
            OnPropertyChanged(nameof(BooksInStore));
            GetStoreInventory();
        }
    }
}
            

