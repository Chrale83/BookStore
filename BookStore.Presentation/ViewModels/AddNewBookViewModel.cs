using BookStore.domain;
using BookStore.Infrastructure.Data.Model;
using BookStore.Presentation.Command;
using BookStore.Presentation.ConnectionDBHandler;
using System.Collections.ObjectModel;

namespace BookStore.Presentation.ViewModels
{
    internal class AddNewBookViewModel : ViewModelBase
    {
        private string? _isbn13;

        public string? Isbn13
        {
            get => _isbn13;
            set
            {
                bool isNumeric = IsInputNumeric(value);
                if (value.Length <= 13 && isNumeric)
                {
                    _isbn13 = value;
                    OnPropertyChanged();
                    ValidIsbn13Check();
                }
            }
        }

        private bool IsInputNumeric(string inputed)
        {
            var tempLength = inputed.Length;

            if (tempLength != 0 && Char.IsDigit(inputed[tempLength - 1]))
            {
                
                return true;
            }
            return false;
        }
        

        private string? _validIsbn13;

        public string? ValidIsbn13
        {
            get => _validIsbn13;
            set
            {
                _validIsbn13 = value;
                OnPropertyChanged();
            }
        }
        public void ValidIsbn13Check()
        {
            if (Isbn13.Length != 13)
            {
                ValidIsbn13 = "Isbn13 need to be 13 letters";
            }
            else
            {
                ValidIsbn13 = "";
            }
            OnPropertyChanged();
        }

        private string? _title;

        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Author>? _authors;

        public ObservableCollection<Author>? Authors
        {
            get => _authors;
            set { _authors = value; }
        }

        private Author _SelectedAutor;

        public Author SelectedAuthor
        {
            get { return _SelectedAutor; }
            set { _SelectedAutor = value; }
        }


        private string? _language;

        public string? Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        private int? _pages;

        public int? Pages
        {
            get => _pages;
            set
            {
                
                _pages = value;
                OnPropertyChanged();
            }
        }

        private int _price;

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _dateReleased;

        public DateOnly DateRealesed
        {
            get => _dateReleased;
            set
            {
                _dateReleased = value;
            }
        }

        private DateTime _pickedDateTime;

        public DateTime PickedDateTime
        {
            get => _pickedDateTime;
            set
            {
                _pickedDateTime = value;
                DateRealesed = DateOnly.FromDateTime(PickedDateTime);
            }
        }

        private ObservableCollection<Publisher> _publishers;

        public ObservableCollection<Publisher> Publishers
        {
            get => _publishers;
            set
            {
                _publishers = value;
            }
        }

        private Publisher _selectedPublisher;

        public Publisher SelectedPublisher
        {
            get => _selectedPublisher;
            set
            {
                _selectedPublisher = value;
            }
        }

        public RelayCommand SaveBookCommand { get; set; }
        public AddNewBookViewModel()
        {
            GetAuthorsFromDb();
            GetPublishersFromDb();
            SaveBookCommand = new RelayCommand(SaveBookToDb);
        }

        private async void GetPublishersFromDb()
        {
            Publishers = await GetDataFromDbHandler.GetPublishersAsync();
        }

        private void SaveBookToDb(object obj)
        {
            Book book = new Book()
            {
                Isbn13 = Isbn13,
                Title = Title,
                Language = Language,
                Price = Price,
                Pages = Pages,
                DateReleased = DateRealesed,
                PublisherId = SelectedPublisher.Id

                //Authors = new List<Author> { SelectedAuthor}
            };

            DataBaseChangeHandler.SaveNewBookToDb(book, SelectedAuthor);

            
        }

        private async void GetAuthorsFromDb()
        {
            Authors = await GetDataFromDbHandler.GetAuthorsAsync();
        }
    }
}












