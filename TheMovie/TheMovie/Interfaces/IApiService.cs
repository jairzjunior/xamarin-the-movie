using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovie.Models;

namespace TheMovie.Interfaces
{
    public interface IApiService
    {
        /// <summary>
        /// Search movies using search term and return an object with list of the movies by page.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<SearchMovie> SearchMoviesAsync(string searchTerm, int page);

        /// <summary>
        /// Search movies using category and return an object with list of the movies by page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<SearchMovie> GetMoviesByCategoryAsync(int page, Enums.MovieCategory sortBy);

        /// <summary>
        /// Get detail of the movie by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MovieDetail> GetMovieDetailAsync(int id);

        /// <summary>
        /// Get list of the genres.
        /// </summary>
        /// <returns></returns>
        Task<List<Genre>> GetGenresAsync();
    }
}