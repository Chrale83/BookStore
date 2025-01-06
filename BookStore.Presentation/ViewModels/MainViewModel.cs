using BookStore.domain;
using BookStore.Presentation.Command;
using System.Windows;

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
        
        public RelayCommand? ShowInventoryCommand { get; private set; }
        public RelayCommand? ShowEditBookCommand { get; private set; }
        public RelayCommand? CloseApplicationCommand { get; private set; }
        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            InventoryViewModel = new InventoryViewModel();
            EditBookStockViewModel = new EditBookStockViewModel();
            
            SelectedViewModel = InventoryViewModel;
            StartRelayCommands();
        }

        private void StartRelayCommands()
        {
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
            SelectedViewModel = EditBookStockViewModel;     
        }

        private void ChangeToInventoryView(object? obj)
        {     
            SelectedViewModel = InventoryViewModel;        
        }
    }
}
