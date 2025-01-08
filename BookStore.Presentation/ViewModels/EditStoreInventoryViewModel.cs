using BookStore.Presentation.Command;

namespace BookStore.Presentation.ViewModels
{
    internal class EditStoreInventoryViewModel : ViewModelBase
    {

        private AddBookToStoreInventoryViewModel AddNewBookToStoreViewModel;
        private RemoveBookFromStoreViewModel DeleteBookFromStoreViewModel;

        private ViewModelBase? _selectedUserView;
        public ViewModelBase? SelectedUserView
        {
            get => _selectedUserView;
            set
            {
                _selectedUserView = value;
                OnPropertyChanged();
            }
        }

        public EditStoreInventoryViewModel()
        {
            AddNewBookToStoreViewModel = new AddBookToStoreInventoryViewModel();
            DeleteBookFromStoreViewModel = new RemoveBookFromStoreViewModel();
            ShowAddBooksCommand = new RelayCommand(SetToAddBookView);
            ShowDeleteBooksCommand = new RelayCommand(SetDeleteBookView);
        }

        private void SetDeleteBookView(object obj)
        {
            SelectedUserView = DeleteBookFromStoreViewModel;
        }

        private void SetToAddBookView(object obj)
        {
            SelectedUserView = AddNewBookToStoreViewModel;
        }

        

        public RelayCommand ShowAddBooksCommand { get; }
        public RelayCommand ShowDeleteBooksCommand { get; }
    }
}
