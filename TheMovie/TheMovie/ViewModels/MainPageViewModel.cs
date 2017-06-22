using Plugin.Connectivity;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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

        public ObservableCollection<Movie> Movies { get; set; }

        public DelegateCommand LoadUpcomingMoviesCommand { get; }
        public DelegateCommand SearchMoviesCommand { get; }        
        public DelegateCommand<Movie> ShowMovieDetailCommand { get; }
        public DelegateCommand<Movie> ItemAppearingCommand { get; }

        private INavigationService navigationService;
        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "TMDb - Upcoming Movies";
            this.navigationService = navigationService;
            Movies = new ObservableCollection<Movie>();

            LoadUpcomingMoviesCommand = new DelegateCommand(ExecuteLoadUpcomingMoviesCommand);
            SearchMoviesCommand = new DelegateCommand(ExecuteSearchMoviesCommand);
            ShowMovieDetailCommand = new DelegateCommand<Movie>(ExecuteShowMovieDetailCommand);
            ItemAppearingCommand = new DelegateCommand<Movie>(ExecuteItemAppearingCommand);

            LoadUpcomingMoviesCommand.Execute();

            IsConnected = CrossConnectivity.Current.IsConnected;
        }        

        private async void ExecuteLoadUpcomingMoviesCommand()
        {
            IsConnected = CrossConnectivity.Current.IsConnected;

            if ((IsBusy) || (!IsConnected))
                return;

            IsBusy = true;

            try
            {
                Movies.Clear();                
                await LoadMoviesAsync(currentPage = 1, Enums.MovieCategory.Upcoming);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteSearchMoviesCommand()
        {            
            await navigationService.NavigateAsync("SearchMoviesPage");
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
            int viewCellIndex = Movies.IndexOf(movie);
            if (Movies.Count - itemLoadNextItem <= viewCellIndex)
            {
                await NextPageUpcomingMoviesAsync();
            }
        }

        private async Task LoadMoviesAsync(int page, Enums.MovieCategory movieCategory)
        {
            genres = genres ?? await ApiService.GetGenresAsync();
            var movies = await ApiService.GetMoviesByCategoryAsync(page, movieCategory);
            if (movies != null)
            {
                totalPage = movies.TotalPages;
                foreach (var item in movies.Movies)
                {
                    GenreListToString(genres, item);
                    Movies.Add(item);
                }
            }            
        }

        public async Task NextPageUpcomingMoviesAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {                
                await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming);
            }
        }

        /// <summary>
        /// Converter the genres of the movies to a string.
        /// </summary>
        /// <param name="genres"></param>
        /// <param name="item"></param>
        private void GenreListToString(List<Genre> genres, Movie item)
        {
            item.GenresNames = "";
            for (int i = 0; i < item.GenreIds.Length; i++)
            {
                var genreId = item.GenreIds[i];                
                item.GenresNames += genres.FirstOrDefault(g => g.Id == genreId)?.Name;
                item.GenresNames += i < (item.GenreIds.Length - 1) ? ", " : "";
            }            
        }        
    }
}