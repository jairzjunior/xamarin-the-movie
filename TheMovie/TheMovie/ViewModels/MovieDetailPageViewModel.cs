using Prism.Navigation;
using System.Threading.Tasks;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class MovieDetailPageViewModel : BaseViewModel, INavigationAware
    {
        private MovieDetail movieDetail;
        public MovieDetail MovieDetail
        {
            get { return movieDetail; }
            set { SetProperty(ref movieDetail, value); }
        }        
  
        private async Task LoadMovieAsync(int movieId)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                MovieDetail = await ApiService.GetMovieDetailAsync(movieId).ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            var movie = parameters.GetValue<Movie>("movie");
            Title = movie.Title;
            await LoadMovieAsync(movie.Id).ConfigureAwait(false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // Do nothing, added because of implementation INavigationAware.
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // Do nothing, added because of implementation INavigationAware.
        }
    }
}