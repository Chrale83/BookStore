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

        public MenuViewModel MenuViewModel { get;  }
        public InventoryViewModel InventoryViewModel { get;  }
        public EditBookStockViewModel EditBookStockViewModel { get;  }
        public AddNewBookViewModel AddNewBookViewModel { get; }
        public RelayCommand? ShowInventoryCommand { get; private set; }
        public RelayCommand? ShowEditBookCommand { get; private set; }
        public RelayCommand? CloseApplicationCommand { get; private set; }
        public RelayCommand? ShowAddNewBookCommand { get; private set; }
        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            InventoryViewModel = new InventoryViewModel();
            EditBookStockViewModel = new EditBookStockViewModel();
            AddNewBookViewModel = new AddNewBookViewModel();
            
            SelectedViewModel = InventoryViewModel;
            StartRelayCommands();
        }

        private void StartRelayCommands()
        {
            ShowInventoryCommand = new RelayCommand(ChangeToInventoryView);
            ShowEditBookCommand = new RelayCommand(ChangeToEditBookView);
            CloseApplicationCommand = new RelayCommand(ShutDownApplication);
            ShowAddNewBookCommand = new RelayCommand(ChangeToAddNewBook);
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
        private void ChangeToAddNewBook(object? obj)
        {
            SelectedViewModel = AddNewBookViewModel;
        }


    }
}
