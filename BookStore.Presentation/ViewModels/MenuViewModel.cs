using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Presentation.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private readonly MainViewModel mainViewModel;

        public MenuViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public ObservableCollection<Store> Stores => mainViewModel.Stores;

        public Store SelectedStore
        {
            get => mainViewModel.SelectedStore;
            set => mainViewModel.SelectedStore = value;
        }
    }
}
