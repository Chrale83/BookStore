using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MenuViewModel MenuViewModel { get; set; }

        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
        }
    }
}
