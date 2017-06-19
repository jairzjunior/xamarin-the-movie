using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TheMovie.Helpers;
using TheMovie.Models;
using TheMovie.Views;
using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {        
        // Variables to control of the pagination
        private int currentPage = 1;
        private int totalPage = 0;        

        public ObservableRangeCollection<Movie> Movies { get; set; }

        public Command LoadUpcomingMoviesCommand { get; }
        public Command SearchMoviesCommand { get; }        
        public Command ShowMovieCommand { get; }

        public MainPageViewModel()
        {            
            Title = "TMDb - Upcoming Movies";
            Movies = new ObservableRangeCollection<Movie>();

            LoadUpcomingMoviesCommand = new Command(ExecuteLoadUpcomingMoviesCommand);
            SearchMoviesCommand = new Command(ExecuteSearchMoviesCommand);
            ShowMovieCommand = new Command<Movie>(ExecuteShowMovieCommand);

            LoadUpcomingMoviesCommand.Execute(null);
        }        

        private async void ExecuteLoadUpcomingMoviesCommand()
        {
            if (IsBusy)
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
            await App.Current.MainPage.Navigation.PushAsync(new SearchMoviesPage());
        }

        private async void ExecuteShowMovieCommand(Movie movie)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MovieDetailPage(new MovieDetailViewModel(movie)));
        }

        private async Task LoadMoviesAsync(int page, Enums.MovieCategory sortBy)
        {
            var genres = await ApiService.GetGenresAsync();
            var searchMovies = await ApiService.GetMoviesAsync(page, sortBy);
            if (searchMovies != null)
            {
                totalPage = searchMovies.TotalPages;
                foreach (var item in searchMovies.Movies)
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