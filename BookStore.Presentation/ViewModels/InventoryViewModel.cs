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
    internal class InventoryViewModel : ViewModelBase
    {
        private Store? _selectedStore;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged();
                //LoadStoreInventory();
            }
        }

        private void LoadStoreInventory()
        {
            if (_selectedStore == null) return;
            
        }
        
    }
}
