using BLL.Interfaces.Repositories;
using Common.DTO;
using Common.Entities;
using DAL.API.Entities;
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

        public async Task<List<ShowDTO>> GetRecentShows()
        {
            Uri url = new Uri("https://localhost:7076/show/recentshows");
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

        public async Task<ShowDTO> GetShowById(int id)
        {
            Uri url = new Uri("https://localhost:7076/show/" + id);
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
