using Plugin.Connectivity;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using TheMovie.Helpers;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {        
        // Variables to control of the pagination
        private int currentPage = 1;
        private int totalPage = 0;

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set { SetProperty(ref isConnected, value); }
        }

        private List<Genre> genres;        
        public ObservableRangeCollection<Movie> Movies { get; set; }

        public DelegateCommand LoadUpcomingMoviesCommand { get; }
        public DelegateCommand ShowSearchMoviesCommand { get; }        
        public DelegateCommand<Movie> ShowMovieDetailCommand { get; }
        public DelegateCommand<Movie> ItemAppearingCommand { get; }

        private readonly INavigationService navigationService;
        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "TMDb - Upcoming Movies";
            this.navigationService = navigationService;
            Movies = new ObservableRangeCollection<Movie>();

            LoadUpcomingMoviesCommand = new DelegateCommand(async () => await ExecuteLoadUpcomingMoviesCommand().ConfigureAwait(false));
            ShowSearchMoviesCommand = new DelegateCommand(async () => await ExecuteShowSearchMoviesCommand().ConfigureAwait(false));
            ShowMovieDetailCommand = new DelegateCommand<Movie>(async (Movie movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));
            ItemAppearingCommand = new DelegateCommand<Movie>(async (Movie movie) => await ExecuteItemAppearingCommand(movie).ConfigureAwait(false));

            LoadUpcomingMoviesCommand.Execute();
        }        

        private async Task ExecuteLoadUpcomingMoviesCommand()
        {
            IsConnected = CrossConnectivity.Current.IsConnected;

            if (IsBusy || !IsConnected)
                return;

            IsBusy = true;

            try
            {
                Movies.Clear();
                currentPage = 1;
                await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming).ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteShowSearchMoviesCommand()
        {            
            await navigationService.NavigateAsync("SearchMoviesPage").ConfigureAwait(false);
        }

        private async Task ExecuteShowMovieDetailCommand(Movie movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };
            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }

        private async Task ExecuteItemAppearingCommand(Movie movie)
        {
            int itemLoadNextItem = 2;
            int viewCellIndex = Movies.IndexOf(movie);
            if (Movies.Count - itemLoadNextItem <= viewCellIndex)
            {                
                await NextPageUpcomingMoviesAsync().ConfigureAwait(false);
            }
        }

        private async Task LoadMoviesAsync(int page, Enums.MovieCategory movieCategory)
        {
            try
            {
                genres = genres ?? await ApiService.GetGenresAsync().ConfigureAwait(false);
                var searchMovies = await ApiService.GetMoviesByCategoryAsync(page, movieCategory).ConfigureAwait(false);
                if (searchMovies != null)
                {
                    var movies = new List<Movie>();
                    totalPage = searchMovies.TotalPages;
                    foreach (var movie in searchMovies.Movies)
                    {
                        movie.Genres = 
                            movie.Genres ??
                            genres.Where(genre => movie.GenreIds.Any(genreId => genreId == genre.Id)).ToArray();

                        movies.Add(movie);
                    }
                    Movies.AddRange(movies);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task NextPageUpcomingMoviesAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {                
                await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming).ConfigureAwait(false);
            }
        }
    }
}
