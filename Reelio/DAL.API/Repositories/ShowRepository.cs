using BLL.Interfaces.Repositories;
using Common.DTO;
using Common.Entities;
using DAL.API.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.API.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private static readonly HttpClient _client = new HttpClient(); 
        private readonly string _apiBaseUrl;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ShowRepository(IConfiguration configuration)
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

        public async Task<PaginatedList<ShowDTO>> GetShows(int pageNumber, int pageSize, string? searchQuery, string? genre)
        {
            var queryString = BuildQueryString(pageNumber, pageSize, searchQuery, genre);
            Uri url = new Uri(_apiBaseUrl + "/Show" + queryString);

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch shows. Status Code: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var paginatedList = JsonSerializer.Deserialize<PaginatedList<Show>>(content, _jsonSerializerOptions);

                if (paginatedList == null)
                    throw new InvalidOperationException("Failed to deserialize paginated list.");

                paginatedList.Items ??= new List<Show>(); // Ensure Items is not null
                var showDTOs = paginatedList.Items.Select(showEntity => new ShowDTO
                {
                    Id = showEntity.Id,
                    Title = showEntity.Title,
                    ReleaseDate = showEntity.ReleaseDate,
                    ImageUrl = showEntity.ImageUrl
                }).ToList();

                return new PaginatedList<ShowDTO>(showDTOs, paginatedList.TotalCount, paginatedList.PageNumber, paginatedList.PageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching shows: {ex.Message}", ex);
            }
        }

        public async Task<ShowDTO> GetShowById(int id)
        {
            Uri url = new Uri(_apiBaseUrl + "/show/" + id);

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch the show with ID {id}. Status Code: {response.StatusCode}");

                var show = JsonSerializer.Deserialize<Show>(content, _jsonSerializerOptions);

                if (show == null)
                    throw new InvalidOperationException("Failed to deserialize the show.");

                var showDTO = new ShowDTO
                {
                    Id = show.Id,
                    Title = show.Title,
                    Description = show.Description,
                    ReleaseDate = show.ReleaseDate,
                    ImageUrl = show.ImageUrl,
                    BackdropUrl = show.BackdropUrl,
                    Seasons = show.Seasons.Select(season => new SeasonDTO
                    {
                        Id = season.Id,
                        SeasonNumber = season.SeasonNumber,
                        Description = season.Description,
                        ReleaseDate = season.ReleaseDate,
                        ShowId = season.ShowId,
                        Episodes = season.Episodes.Select(episode => new EpisodeDTO
                        {
                            Id = episode.Id,
                            Title = episode.Title,
                            Description = episode.Description,
                            ReleaseDate = episode.ReleaseDate,
                            Director = episode.Director,
                            EpisodeNumber = episode.EpisodeNumber
                        }).ToList()
                    }).ToList()
                };

                return showDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching show by ID {id}: {ex.Message}", ex);
            }
        }
    }
}
