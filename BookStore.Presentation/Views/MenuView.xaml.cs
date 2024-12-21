using BookStore.Presentation.ViewModels;
using System.Windows.Controls;

namespace BookStore.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            //DataContext = App.Current.MainWindow.DataContext;
            //this.DataContext = new MenuViewModel();

        }
    }
}
