using BookStore.Presentation.Command;
using System.Windows;
using System.Windows.Input;

namespace BookStore.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;
        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel MenuViewModel { get; set; }
        public InventoryViewModel InventoryViewModel { get; set; }
        public EditBookStockViewModel EditBookStockViewModel { get; set; }
        
        public RelayCommand ShowInventoryCommand { get; }
        public RelayCommand ShowEditBookCommand { get; }
        public RelayCommand CloseApplicationCommand { get; }
        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            InventoryViewModel = new InventoryViewModel();
            EditBookStockViewModel = new EditBookStockViewModel();
            
            SelectedViewModel = InventoryViewModel;
            
            ShowInventoryCommand = new RelayCommand(ChangeToInventoryView);
            ShowEditBookCommand = new RelayCommand(ChangeToEditBookView);
            CloseApplicationCommand = new RelayCommand(ShutDownApplication);

        }

        private void ShutDownApplication(object? obj)
        {
            Application.Current.Shutdown();
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

        
    }
}
