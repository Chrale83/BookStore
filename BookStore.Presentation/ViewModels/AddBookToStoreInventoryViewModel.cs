using BookStore.domain;
using BookStore.Presentation.Command;
using BookStore.Presentation.ConnectionDBHandler;
using BookStore.Presentation.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class AddBookToStoreInventoryViewModel : ViewModelBase
    {
        private Store _selectedStore;
        private ObservableCollection<Book> _allBooksInStores;
        private Book _SelectedBook;
        public Store SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                GetAllBooks();
            }
        }
        public ObservableCollection<Book> AllBooksInStores
        {
            get => _allBooksInStores;
            set
            {
                _allBooksInStores = value;
                AddBookToStoreCommand.RaiseCanExectueChanged();
            }
        }
        public Book SelectedBook
        {
            get => _SelectedBook;
            set
            {
                _SelectedBook = value;
                OnPropertyChanged();
                AddBookToStoreCommand.RaiseCanExectueChanged();
            }
        }
        public AddBookToStoreInventoryViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedStoreMessage>(this, async (r, message) =>
            {
                SelectedStore = message.SelectedStore;
                
            });
            AddBookToStoreCommand = new RelayCommand(SaveBookToStore, CanSaveBookToStore);
        }

        private bool CanSaveBookToStore(object? arg)
        {
            return SelectedStore != null && SelectedBook != null;
        }

        public RelayCommand AddBookToStoreCommand { get; }
        public async void GetAllBooks()
        {
            if (SelectedStore != null)
            {
                AllBooksInStores = await GetDataFromDbHandler.GetAllBooksFromDbAsync(SelectedStore.Id);
                OnPropertyChanged(nameof(AllBooksInStores));
            }
        }
        public async void SaveBookToStore(object? arg)
        {
            await SetDataToDbHandler.AddBookToStore(SelectedBook.Isbn13, SelectedStore.Id);
            GetAllBooks();
            OnPropertyChanged(nameof(AllBooksInStores));
        }
    }
}











