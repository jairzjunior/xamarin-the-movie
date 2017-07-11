using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using TheMovie.Helpers;
using TheMovie.Models;
using Xamarin.Forms;

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
            get 
            { 
                return searchTerm;
            }
            set
            {
                SetProperty(ref searchTerm, value);                
                SearchResults.Clear();
            }
        }

        public ObservableRangeCollection<Movie> SearchResults { get; set; }

        public DelegateCommand SearchCommand { get; }
        public DelegateCommand<Movie> ShowMovieDetailCommand { get; }
        public DelegateCommand<Movie> ItemAppearingCommand { get; }

        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        public SearchMoviesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            Title = "Search Movies";
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            SearchResults = new ObservableRangeCollection<Movie>();
            
            SearchCommand = new DelegateCommand(async () => await ExecuteSearchCommand().ConfigureAwait(false));
            ShowMovieDetailCommand = new DelegateCommand<Movie>(async (Movie movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));
            ItemAppearingCommand = new DelegateCommand<Movie>(async (Movie movie) => await ExecuteItemAppearingCommand(movie).ConfigureAwait(false));
        }

        private async Task ExecuteSearchCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                SearchResults.Clear();
                currentPage = 1;
                await LoadAsync(currentPage).ConfigureAwait(true);
            }
            finally
            {
                IsBusy = false;
            }

            if (SearchResults.Count == 0)
            {
                await pageDialogService.DisplayAlertAsync("The Movie", "No results found.", "Ok").ConfigureAwait(false);                
            }
        }

        private async Task ExecuteShowMovieDetailCommand(Movie movie)
        {            
            var parameters = new NavigationParameters();
            parameters.Add(nameof(movie), movie);
            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }

        private async Task ExecuteItemAppearingCommand(Movie movie)
        {            
            int itemLoadNextItem = 2;
            int viewCellIndex = SearchResults.IndexOf(movie);
            if (SearchResults.Count - itemLoadNextItem <= viewCellIndex)
            {
                await NextPageAsync().ConfigureAwait(false);
            }
        }

        private async Task NextPageAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadAsync(currentPage).ConfigureAwait(false);
            }
        }

        private async Task LoadAsync(int page)
        {
            try
            {
                // Added to configure "ConfigureAwait(true)" on Windows                
                var continueOnCapturedContext = Device.RuntimePlatform == Device.Windows;

                var searchMovies = await ApiService.SearchMoviesAsync(searchTerm, page).ConfigureAwait(continueOnCapturedContext);

                if (searchMovies != null)
                {
                    totalPage = searchMovies.TotalPages;                    
                    SearchResults.AddRange(searchMovies.Movies);                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }        
    }
}