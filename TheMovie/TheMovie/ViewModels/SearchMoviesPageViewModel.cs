using System.Threading.Tasks;

using TheMovie.Helpers;
using TheMovie.Models;
using TheMovie.Views;
using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class SearchMoviesViewModel : BaseViewModel
    {
        // Variables to control of the pagination
        private int currentPage = 1;
        private int totalPage = 0;

        private string searchTerm;
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                SetProperty(ref searchTerm, value);
                SearchCommand.ChangeCanExecute();
                SearchResults.Clear();
            }
        }

        public ObservableRangeCollection<Movie> SearchResults { get; set; }

        public Command SearchCommand { get; }
        public Command ShowMovieCommand { get; }

        public SearchMoviesViewModel()
        {
            Title = "Search Movies";
            SearchResults = new ObservableRangeCollection<Movie>();            
            
            SearchCommand = new Command(ExecuteSearchCommand);
            ShowMovieCommand = new Command<Movie>(ExecuteShowMovieCommand);
        }        

        private async void ExecuteSearchCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                SearchResults.Clear();
                await LoadAsync(currentPage = 1);
            }
            finally
            {
                IsBusy = false;
            }

            if (SearchResults.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("The Movie", "No results found.", "Ok");
                return;
            }
        }

        private async void ExecuteShowMovieCommand(Movie movie)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MovieDetailPage(new MovieDetailViewModel(movie)));
        }

        public async Task NextPageAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadAsync(currentPage);
            }
        }

        private async Task LoadAsync(int page)
        {
            var searchMovies = await ApiService.SearchMoviesAsync(searchTerm, page);

            if (searchMovies != null)
            {
                totalPage = searchMovies.TotalPages;
                SearchResults.AddRange(searchMovies.Movies);
            }                
        }
    }
}