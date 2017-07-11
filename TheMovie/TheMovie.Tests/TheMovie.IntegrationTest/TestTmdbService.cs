using NUnit.Framework;
using System.Threading.Tasks;
using TheMovie.Interfaces;
using TheMovie.Services;

namespace TheMovie.IntegrationTest
{
    [TestFixture]
    public class TestTmdbService
    {
        private IApiService apiService;

        public TestTmdbService()
        {
            apiService = new TmdbService();
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetGenres()
        {
            var genres = await apiService.GetGenresAsync();
            Assert.AreNotEqual(0, genres.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetMovieDetail()
        {
            const int movieId = 343611;

            var movie = await apiService.GetMovieDetailAsync(movieId);
            Assert.IsNotNull(movie);
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesNowPlaying()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.NowPlaying);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesPopular()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.Popular);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesTopRated()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.TopRated);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesUpcoming()
        {
            const int page = 1;

            var searchMovie = await apiService.GetMoviesByCategoryAsync(page, Models.Enums.MovieCategory.Upcoming);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task SearchMovies()
        {
            const string searchTerm = "abc";
            const int page = 1;

            var searchMovie = await apiService.SearchMoviesAsync(searchTerm, page);
            Assert.IsNotNull(searchMovie);
            Assert.AreNotEqual(0, searchMovie.Movies.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task SearchMoviesWithoutResult()
        {
            const string searchTerm = "_WithoutResult_";
            const int page = 1;

            var searchMovie = await apiService.SearchMoviesAsync(searchTerm, page);
            Assert.IsNotNull(searchMovie);
            Assert.AreEqual(0, searchMovie.Movies.Count);
        }

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesNowPlayingPagination()
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

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesPopularPagination()
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

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesTopRatedPagination()
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

        [Test]
        [Category("Integration Test")]
        public async Task GetMoviesUpcomingPaginationTest()
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

        [Test]
        [Category("Integration Test")]
        public async Task SearchMoviesPaginationTest()
        {
            const string searchTerm = "abc";

            var searchMovie = await apiService.SearchMoviesAsync(searchTerm, 1);
            var totalPages = searchMovie.TotalPages;
            for (int i = 1; i <= totalPages; i++)
            {
                searchMovie = await apiService.SearchMoviesAsync(searchTerm, i);
                Assert.IsNotNull(searchMovie);
                Assert.AreNotEqual(0, searchMovie.Movies.Count);
            }
        }
    }
}
