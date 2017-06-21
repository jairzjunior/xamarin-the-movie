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
            const int movieId = 343611;

            var movie = await apiService.GetMovieDetailAsync(movieId);
            Assert.IsNotNull(movie);
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesNowPlayingAsync()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.NowPlaying);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesPopularAsync()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.Popular);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);            
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesTopRatedAsync()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.TopRated);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [TestMethod]
        [TestCategory("First page")]
        public async Task GetMoviesUpcomingAsync()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.Upcoming);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }        

        [TestMethod]
        [TestCategory("First page")]
        public async Task SearchMoviesAsync()
        {
            const string searchTerm = "abc";
            const int page = 1;

            var searchMovie = await apiService.SearchMoviesAsync(searchTerm, page);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }
        
        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesNowPlayingPaginationAsync()
        {
            var searchMovie = await apiService.GetMoviesByCategoryAsync(1, Models.Enums.MovieCategory.NowPlaying);
            var totalPages = searchMovie.TotalPages;
            for (int i = 1; i <= totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesByCategoryAsync(i, Models.Enums.MovieCategory.NowPlaying);
                Assert.AreNotEqual(null, searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesPopularPaginationAsync()
        {
            var searchMovie = await apiService.GetMoviesByCategoryAsync(1, Models.Enums.MovieCategory.Popular);            
            var totalPages = searchMovie.TotalPages;            
            for (int i = 1; i <= totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesByCategoryAsync(i, Models.Enums.MovieCategory.Popular);
                Assert.IsNotNull(searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesTopRatedPaginationAsync()
        {
            var searchMovie = await apiService.GetMoviesByCategoryAsync(1, Models.Enums.MovieCategory.TopRated);            
            var totalPages = searchMovie.TotalPages;
            for (int i = 1; i <= totalPages; i++)
            {                
                searchMovie = await apiService.GetMoviesByCategoryAsync(i, Models.Enums.MovieCategory.TopRated);
                Assert.IsNotNull(searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task GetMoviesUpcomingPaginationTestAsync()
        {
            var searchMovie = await apiService.GetMoviesByCategoryAsync(1, Models.Enums.MovieCategory.Upcoming);
            var totalPages = searchMovie.TotalPages;
            for (int i = 1; i <= totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesByCategoryAsync(i, Models.Enums.MovieCategory.Upcoming);
                Assert.AreNotEqual(null, searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }

        [TestMethod]
        [TestCategory("Pagination")]
        public async Task SearchMoviesPaginationTestAsync()
        {
            const string searchTerm = "abc";

            var searchMovie = await apiService.SearchMoviesAsync(searchTerm, 1);
            var totalPages = searchMovie.TotalPages;
            for (int i = 1; i <= totalPages; i++)
            {
                searchMovie = await apiService.GetMoviesByCategoryAsync(i, Models.Enums.MovieCategory.Upcoming);
                Assert.IsNotNull(searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }        
    }
}
