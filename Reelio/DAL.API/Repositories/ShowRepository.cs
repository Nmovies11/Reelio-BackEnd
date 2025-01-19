using BLL.Interfaces.Repositories;
using Common.DTO;
using Common.Entities;
using DAL.API.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace DAL.API.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiBaseUrl;

        public ShowRepository(IConfiguration configuration)
        {
            _apiBaseUrl = configuration["NMDB_URL"];
        }


        public async Task<List<ShowDTO>> GetRecentShows()
        {
            Uri url = new Uri(_apiBaseUrl + "/show/recentshows");
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);


            var shows = JsonSerializer.Deserialize<List<Show>>(content);

            List<ShowDTO> showDTOs = new List<ShowDTO>();
            foreach (var show in showDTOs)
            {
                ShowDTO showDAO = new ShowDTO
                {
                    Id = show.Id,
                    Title = show.Title,
                    Description = show.Description,
                    ReleaseDate = show.ReleaseDate,
                    ImageUrl = show.ImageUrl,
                    BackdropUrl = show.BackdropUrl,
                };
                showDTOs.Add(showDAO);
            }


            return showDTOs;

        }

        public async Task<PaginatedList<ShowDTO>> GetShows(int pageNumber, int pageSize, string? searchQuery, string? genre)
        {
            var queryString = $"?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                queryString += $"&searchQuery={Uri.EscapeDataString(searchQuery)}";
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                queryString += $"&genre={Uri.EscapeDataString(genre)}";
            }

            Uri url = new Uri(_apiBaseUrl + "/Show" + queryString);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch shows. Status Code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + content);

            var paginatedList = JsonSerializer.Deserialize<PaginatedList<Show>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            });

            if (paginatedList == null)
            {
                throw new InvalidOperationException("Failed to deserialize paginated list.");
            }

            paginatedList.Items ??= new List<Show>();

            var showDTOs = paginatedList.Items.Select(showEntity => new ShowDTO
            {
                Id = showEntity.Id,
                Title = showEntity.Title,
                ReleaseDate = showEntity.ReleaseDate,
                ImageUrl = showEntity.ImageUrl
            }).ToList();

            return new PaginatedList<ShowDTO>(showDTOs, paginatedList.TotalCount, paginatedList.PageNumber, paginatedList.PageSize);
        }


        public async Task<ShowDTO> GetShowById(int id)
        {
            Uri url = new Uri(_apiBaseUrl + "/show/" + id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var show = JsonSerializer.Deserialize<Show>(content);

            ShowDTO showDTO = new ShowDTO
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

    }
}
