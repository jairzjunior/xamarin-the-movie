using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class SearchMoviesPageViewModel : BaseViewModel
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
                SearchResults.Clear();
            }
        }

        public ObservableCollection<Movie> SearchResults { get; set; }

        public DelegateCommand SearchCommand { get; }
        public DelegateCommand<Movie> ShowMovieDetailCommand { get; }
        public DelegateCommand<Movie> ItemAppearingCommand { get; }

        private INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        public SearchMoviesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            Title = "Search Movies";
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            SearchResults = new ObservableCollection<Movie>();
            
            SearchCommand = new DelegateCommand(ExecuteSearchCommand);
            ShowMovieDetailCommand = new DelegateCommand<Movie>(ExecuteShowMovieDetailCommand);
            ItemAppearingCommand = new DelegateCommand<Movie>(ExecuteItemAppearingCommand);
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
                await pageDialogService.DisplayAlertAsync("The Movie", "No results found.", "Ok");
                return;
            }
        }

        private async void ExecuteShowMovieDetailCommand(Movie movie)
        {            
            var p = new NavigationParameters();
            p.Add(nameof(movie), movie);
            await navigationService.NavigateAsync("MovieDetailPage", p);
        }

        private async void ExecuteItemAppearingCommand(Movie movie)
        {            
            int itemLoadNextItem = 2;
            int viewCellIndex = SearchResults.IndexOf(movie);
            if (SearchResults.Count - itemLoadNextItem <= viewCellIndex)
            {
                await NextPageAsync();
            }
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
                foreach (var movie in searchMovies.Movies)
                {
                    SearchResults.Add(movie);
                }                               
            }                
        }        
    }
}