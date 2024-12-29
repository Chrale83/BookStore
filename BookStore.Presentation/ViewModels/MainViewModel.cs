﻿using BookStore.Presentation.Command;
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
        public AddBookViewModel AddBookViewModel { get; set; }
        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            InventoryViewModel = new InventoryViewModel();
            AddBookViewModel = new AddBookViewModel();
            
            SelectedViewModel = InventoryViewModel;
            
            ShowInventoryCommand = new RelayCommand(ChangeToInventoryView);
            ShowAddBookCommand = new RelayCommand(ChangeToAddBookView);

        }

        private void ChangeToAddBookView(object? obj)
        {
            
            AddBookViewModel.LoadBookTitles();
            SelectedViewModel = AddBookViewModel;
            
        }

        private void ChangeToInventoryView(object? obj)
        {
            InventoryViewModel.LoadStoreStock();
            SelectedViewModel = InventoryViewModel;
            
            
        }

        public ICommand ShowInventoryCommand { get; }
        public ICommand ShowAddBookCommand { get; }

        
    }
}
