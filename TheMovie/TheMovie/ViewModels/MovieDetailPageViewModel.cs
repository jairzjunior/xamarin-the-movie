using System.Threading.Tasks;
using TheMovie.Models;
using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel
    {
        private MovieDetail movieDetail;
        public MovieDetail MovieDetail
        {
            get { return movieDetail; }
            set { SetProperty(ref movieDetail, value); }
        }

        private bool isDone = false;
        public bool IsDone
        {
            get { return isDone; }
            set { SetProperty(ref isDone, value); }
        }

        public MovieDetailViewModel(Movie movie)
        {
            this.Title = movie.Title;            

            var loadMovieCommand = new Command<int>(ExecuteLoadMovieCommand);
            loadMovieCommand.Execute(movie.Id);
        }
    
        private async void ExecuteLoadMovieCommand(int movieId)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                MovieDetail = await ApiService.GetMovieDetailAsync(movieId);
            }
            finally
            {
                IsBusy = false;                
                IsDone = true;
            }
        }
    }
}