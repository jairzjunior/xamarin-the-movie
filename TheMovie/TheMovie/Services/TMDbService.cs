using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheMovie.Models;
using Xamarin.Forms;
using TheMovie.Interfaces;
using System.Net.Http.Headers;
using System.IO;
using System.Globalization;
using System.Diagnostics;

[assembly: Dependency(typeof(TheMovie.Services.TMDbService))]
namespace TheMovie.Services
{
    public class TMDbService : IApiService
    {
        private const string apiKey = "1f54bd990f1cdfb230adb312546d765d";
        private const string baseUrl = "https://api.themoviedb.org/3";

        private const string searchMoviePath = "/search/movie";
        private const string moviePath = "/movie";
        private const string genreListPath = "/genre/list";

        private readonly string language = CultureInfo.CurrentCulture.Name;

        /// <summary>
        /// Search movies using search term and return an object with list of the movies by page.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<SearchMovie> SearchMoviesAsync(string searchTerm, int page)
        {            
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var restUrl = $"{baseUrl}{searchMoviePath}?api_key={apiKey}&query={searchTerm}&page={page}&language={language}";

            try
            {
                var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<SearchMovie>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }
        
        /// <summary>
        /// Search movies using category and return an object with list of the movies by page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<SearchMovie> GetMoviesAsync(int page, Enums.MovieCategory category)
        {            
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var restUrl = $"{baseUrl}{Enums.PathCategoryMovie(category)}?api_key={apiKey}&page={page}&language={language}";

            try
            {
                var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<SearchMovie>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }


        /// <summary>
        /// Get detail of the movie by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MovieDetail> GetMovieDetailAsync(int id)
        {            
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var restUrl = $"{baseUrl}{moviePath}/{id}?api_key={apiKey}&language={language}";

            try
            {
                var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<MovieDetail>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            return null;
        }

        /// <summary>
        /// Get list of the genres.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Genre>> GetGenresAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var restUrl = $"{baseUrl}{genreListPath}?api_key={apiKey}&language={language}";
            try
            {
                var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        var genreList = JsonConvert.DeserializeObject<GenreList>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                        return genreList?.Genres;
                    }
                }
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