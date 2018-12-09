using Mobile.Consts;
using Mobile.Extensions;
using Plugin.Share;
using Prism.Commands;
using Prism.Navigation;
using System.Linq;
using System.Threading.Tasks;
using TheMovie.Helpers;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class MovieDetailPageViewModel : BaseViewModel, INavigatingAware
    {
        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set { SetProperty(ref movie, value); }
        }

        private int heightVideos = 200;
        public int HeightVideos
        {
            get { return heightVideos; }
            set { SetProperty(ref heightVideos, value); }
        }

        public ObservableRangeCollection<Image> Backdrops { get; set; } = new ObservableRangeCollection<Image>();
        public ObservableRangeCollection<Video> Videos { get; set; } = new ObservableRangeCollection<Video>();

        public DelegateCommand ShareCommand { get; }
        public DelegateCommand<Video> OpenVideoCommand { get; }        

        public MovieDetailPageViewModel()
        {
            ShareCommand = new DelegateCommand(async () => await ShareCommandExecuteAsync());
            OpenVideoCommand = new DelegateCommand<Video>(async (Video video) => await ExecuteOpenVideoCommandAsync(video).ConfigureAwait(false));
        }

        private async Task ShareCommandExecuteAsync()
        {
            await CrossShare.Current.Share(
                new Plugin.Share.Abstractions.ShareMessage
                {
                    Text = movie.Title,
                    Title = ConfigApp.ProjectName,
                    Url = $"http://{ConfigApp.WebUrl}{ConfigApp.MovieSharedPath}{movie.Id}"
                }
            );
        }

        private async Task ExecuteOpenVideoCommandAsync(Video video)
        {
            await CrossShare.Current.OpenBrowserAsync($"https://www.youtube.com/watch?v={video.Key}");
        }

        private async Task LoadMovieDetailAsync(int movieId)
        {
            var movieDetail = await ApiService.GetMovieDetailAsync(movieId).ConfigureAwait(false);
            if (movieDetail != null)
            {
                Movie = movieDetail;
            }            
        }

        private async Task LoadMovieImagesAsync(int movieId)
        {
            var movieImages = await ApiService.GetMovieImagesAsync(movieId).ConfigureAwait(false);
            if (movieImages != null)
            {
                Backdrops.AddRange(movieImages.Backdrops.Where(x => x.FilePath != movie.BackdropPath));
            }
        }

        private async Task LoadMovieVideosAsync(int movieId)
        {
            var movieVideos = await ApiService.GetMovieVideosAsync(movieId).ConfigureAwait(false);
            if (movieVideos != null)
            {
                Videos.AddRange(movieVideos.Results);
                HeightVideos = Videos.Count * HeightVideos;
            }
        }

        public async void OnNavigatingTo(INavigationParameters parameters)
        {
            Movie = parameters.GetValue<Movie>("movie");            
            Title = Movie.Title;

            Backdrops.Clear();
            Backdrops.Add(new Image
            {
                FilePath = movie.BackdropPath
            });            

            await LoadMovieDetailAsync(Movie.Id).ConfigureAwait(false);
            await LoadMovieImagesAsync(Movie.Id).ConfigureAwait(false);
            await LoadMovieVideosAsync(Movie.Id).ConfigureAwait(false);
        }
    }
}
