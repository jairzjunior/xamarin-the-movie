using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TheMovie.Interfaces;
using TheMovie.Services;

namespace TheMovie.UnitTest
{
    [TestClass]
    public class TestTMDbService
    {
        private IApiService apiService;

        public TestTMDbService()
        {
            apiService = new TMDbService();
        }        

        [TestMethod]
        public async Task GetGenresAsync()
        {            
            var genres = await apiService.GetGenresAsync();
            Assert.AreNotEqual(0, genres.Count);
        }

        [TestMethod]
        public async Task GetMovieDetailAsync()
        {            
            var movie = await apiService.GetMovieDetailAsync(343611);
            Assert.AreNotEqual(null, movie);
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesNowPlayingAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.NowPlaying);
            Assert.AreNotEqual(null, searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesPopularAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Popular);
            Assert.AreNotEqual(null, searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);            
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesTopRatedAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.TopRated);
            Assert.AreNotEqual(null, searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesUpcomingAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Upcoming);
            Assert.AreNotEqual(null, searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }        

        [TestMethod]
        [TestCategory("First page")]
        public async Task SearchMoviesAsync()
        {
            var searchMovie = await apiService.SearchMoviesAsync("abc", 1);
            Assert.AreNotEqual(null, searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }
        
        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesNowPlayingPaginationAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.NowPlaying);
            Assert.AreNotEqual(null, searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesPopularPaginationAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Popular);            
            var totalPages = searchMovie.TotalPages;            
            for (int i = 0; i < totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Popular);
                Assert.AreNotEqual(null, searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesTopRatedPaginationAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.TopRated);            
            var totalPages = searchMovie.TotalPages;
            for (int i = 0; i < totalPages; i++)
            {                
                searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.TopRated);
                Assert.AreNotEqual(null, searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesUpcomingPaginationTestAsync()
        {
            var searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Upcoming);
            var totalPages = searchMovie.TotalPages;
            for (int i = 0; i < totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Upcoming);
                Assert.AreNotEqual(null, searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task SearchMoviesPaginationTestAsync()
        {
            var searchMovie = await apiService.SearchMoviesAsync("abc", 1);
            var totalPages = searchMovie.TotalPages;
            for (int i = 0; i < totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesAsync(1, Models.Enums.MovieCategory.Upcoming);
                Assert.AreNotEqual(null, searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }        
    }
}
