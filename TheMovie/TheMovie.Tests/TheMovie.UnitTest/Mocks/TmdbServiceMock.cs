using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovie.Models;
using TheMovie.Interfaces;
using System.Globalization;
using System.Diagnostics;
using System.Linq;

namespace TheMovie.UnitTest.Mocks
{
    public class TmdbServiceMock : IApiService
    {       
        private readonly string language = CultureInfo.CurrentCulture.Name;
        private List<Movie> movies;
        private List<Genre> genres;
        private const int numberMoviesByPage = 20;

        private int lastIndexTitle = 0;
        private string GetTitle()
        {
            string[] title = {
                "Title",
                Enums.NameCategoryMovie(Enums.MovieCategory.NowPlaying),
                Enums.NameCategoryMovie(Enums.MovieCategory.Popular),
                Enums.NameCategoryMovie(Enums.MovieCategory.TopRated),
                Enums.NameCategoryMovie(Enums.MovieCategory.Upcoming)
            };
            lastIndexTitle++;
            if (lastIndexTitle >= title.Length)
            {
                lastIndexTitle = 0;
            }

            return title[lastIndexTitle];
        }

        public TmdbServiceMock()
        {
            movies = new List<Movie>();
            for (int i = 0; i < 1000; i++)
            {
                movies.Add(new Movie()
                {
                    Id = i,
                    Title = $"{GetTitle()} {i}"
                });
            }                                    

            genres = new List<Genre>();
            for (int i = 0; i < 100; i++)
            {
                genres.Add(new Genre()
                {
                    Id = i,
                    Name = $"Genre {i}"
                });
            }
        }

        public Task<SearchMovie> SearchMoviesAsync(string searchTerm, int page)
        {                        
            try
            {
                var searchMovie = new SearchMovie();
                var moviesSearch = movies.Where(m => m.Title.ToLower().Contains(searchTerm.ToLower())).ToList();

                var countSkip = numberMoviesByPage * (page - 1);
                searchMovie.Movies = moviesSearch.Skip(countSkip).Take(numberMoviesByPage).ToList();

                searchMovie.TotalResults = moviesSearch.Count();
                searchMovie.TotalPages = moviesSearch.Count() / numberMoviesByPage;

                return Task.FromResult(searchMovie);
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }
        
        public Task<SearchMovie> GetMoviesByCategoryAsync(int page, Enums.MovieCategory category)
        {
            try
            {
                var searchMovie = new SearchMovie();
                var moviesSearch = movies.Where(m => m.Title.ToLower().Contains(Enums.NameCategoryMovie(category).ToLower())).ToList();

                var countSkip = numberMoviesByPage * (page - 1);
                searchMovie.Movies = moviesSearch.Skip(countSkip).Take(numberMoviesByPage).ToList();

                searchMovie.TotalResults = moviesSearch.Count();
                searchMovie.TotalPages = moviesSearch.Count() / numberMoviesByPage;

                return Task.FromResult(searchMovie);
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        public Task<MovieDetail> GetMovieDetailAsync(int id)
        {
            try
            {
                var movie = movies.FirstOrDefault(m => m.Id.Equals(id));
                MovieDetail movieDetail = new MovieDetail()
                {                    
                    Id = movie.Id,
                    Title = movie.Title
                };                
                return Task.FromResult(movieDetail);
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        public Task<List<Genre>> GetGenresAsync()
        {
            try
            {
                return Task.FromResult(genres);
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        private void ReportError(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }    
}