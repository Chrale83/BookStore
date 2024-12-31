using BookStore.Presentation.Command;
using System.Windows.Input;

namespace BookStore.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }
                
        public MenuViewModel MenuViewModel { get; set; }
        public InventoryViewModel InventoryViewModel { get; set; }
        public EditBookStockViewModel EditBookStockViewModel { get; set; }
        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            InventoryViewModel = new InventoryViewModel();
            EditBookStockViewModel = new EditBookStockViewModel();
            
            SelectedViewModel = InventoryViewModel;
            
            ShowInventoryCommand = new RelayCommand(ChangeToInventoryView);
            ShowEditBookCommand = new RelayCommand(ChangeToEditBookView);

        }

        private void ChangeToEditBookView(object? obj)
        {
            
            EditBookStockViewModel.LoadBookTitles();
            SelectedViewModel = EditBookStockViewModel;
            
        }

        private void ChangeToInventoryView(object? obj)
        {
            InventoryViewModel.LoadStoreStock();
            SelectedViewModel = InventoryViewModel;
            
            
        }

        public ICommand ShowInventoryCommand { get; }
        public ICommand ShowEditBookCommand { get; }

        
    }
}
