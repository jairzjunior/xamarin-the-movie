using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovie.Models;

namespace TheMovie.Interfaces
{
    public interface IApiService
    {
        Task<SearchMovie> SearchMoviesAsync(string searchTerm, int page);
        Task<SearchMovie> GetMoviesAsync(int page, Enums.MovieCategory sortBy);        
        Task<MovieDetail> GetMovieDetailAsync(int id);
        Task<List<Genre>> GetGenresAsync();
    }
}