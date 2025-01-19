using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using Common.Entities;
using Common.DTO;
using Microsoft.Extensions.Configuration;
using DAL.API.Entities;

namespace DAL.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private static readonly HttpClient _client = new HttpClient(); 
        private readonly string _apiBaseUrl;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MovieRepository(IConfiguration configuration)
        {
            _apiBaseUrl = configuration["NMDB_URL"];
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        private string BuildQueryString(int pageNumber, int pageSize, string? searchQuery, string? genre)
        {
            var queryString = $"?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(searchQuery))
                queryString += $"&searchQuery={Uri.EscapeDataString(searchQuery)}";

            if (!string.IsNullOrWhiteSpace(genre))
                queryString += $"&genre={Uri.EscapeDataString(genre)}";

            return queryString;
        }

        public async Task<List<MovieDTO>> GetRecentMovies()
        {
            Uri url = new Uri(_apiBaseUrl + "/movie/recentmovies");

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch recent movies. Status Code: {response.StatusCode}");

                var movieEntities = JsonSerializer.Deserialize<List<Movie>>(content, _jsonSerializerOptions);

                if (movieEntities == null || !movieEntities.Any())
                    return new List<MovieDTO>();

                return movieEntities.Select(movieEntity => new MovieDTO
                {
                    Id = movieEntity.Id,
                    Title = movieEntity.Title,
                    ReleaseDate = movieEntity.ReleaseDate,
                    Director = movieEntity.Director,
                    ImageUrl = movieEntity.ImageUrl
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching recent movies: {ex.Message}", ex);
            }
        }

        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            Uri url = new Uri(_apiBaseUrl + "/movie/" + id);

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch movie with ID {id}. Status Code: {response.StatusCode}");

                var movieEntity = JsonSerializer.Deserialize<Movie>(content, _jsonSerializerOptions);

                if (movieEntity == null)
                    throw new InvalidOperationException("Failed to deserialize movie.");

                var movieDTO = new MovieDTODetails
                {
                    Id = movieEntity.Id,
                    Title = movieEntity.Title,
                    Description = movieEntity.Description,
                    ReleaseDate = movieEntity.ReleaseDate,
                    Director = movieEntity.Director,
                    ImageUrl = movieEntity.ImageUrl,
                    BackdropUrl = movieEntity.BackdropUrl,
                    Runtime = movieEntity.Runtime,
                    Actors = ConvertActors(movieEntity.Actors)
                };

                return movieDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching movie by ID {id}: {ex.Message}", ex);
            }
        }

        public List<ActorDTO> ConvertActors(ICollection<Actor> actorDTOs)
        {
            if (actorDTOs == null)
                return new List<ActorDTO>();

            return actorDTOs.Select(actorDTO => new ActorDTO
            {
                Id = actorDTO.Id,
                Name = actorDTO.Name,
                ImageUrl = actorDTO.ImageUrl,
                Role = actorDTO.Role
            }).ToList();
        }

        public async Task<PaginatedList<MovieDTO>> GetMovies(int pageNumber, int pageSize, string? searchQuery, string? genre)
        {
            var queryString = BuildQueryString(pageNumber, pageSize, searchQuery, genre);
            Uri url = new Uri(_apiBaseUrl + "/Movie" + queryString);

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch movies. Status Code: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var paginatedList = JsonSerializer.Deserialize<PaginatedList<Movie>>(content, _jsonSerializerOptions);

                if (paginatedList == null)
                    throw new InvalidOperationException("Failed to deserialize paginated list.");

                paginatedList.Items ??= new List<Movie>();

                var movieDTOs = paginatedList.Items.Select(movieEntity => new MovieDTO
                {
                    Id = movieEntity.Id,
                    Title = movieEntity.Title,
                    ReleaseDate = movieEntity.ReleaseDate,
                    Director = movieEntity.Director,
                    ImageUrl = movieEntity.ImageUrl
                }).ToList();

                return new PaginatedList<MovieDTO>(movieDTOs, paginatedList.TotalCount, paginatedList.PageNumber, paginatedList.PageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching movies: {ex.Message}", ex);
            }
        }
    }
}
