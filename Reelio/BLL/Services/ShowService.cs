using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.DTO;
using Common.Entities;

namespace BLL.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public async Task<List<ShowDTO>> GetRecentShows()
        {

            var showEntities = await _showRepository.GetRecentShows();

            List<ShowDTO> showDAOs = new List<ShowDTO>();
            foreach (var show in showEntities)
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
                showDAOs.Add(showDAO);
            }
            return showDAOs;
        }

        public async Task<ShowDTO> GetShowById(int id)
        {
            var showEntity = await _showRepository.GetShowById(id);
            ShowDTO showDAO = new ShowDTO
            {
                Id = showEntity.Id,
                Title = showEntity.Title,
                Description = showEntity.Description,
                ReleaseDate = showEntity.ReleaseDate,
                ImageUrl = showEntity.ImageUrl,
                BackdropUrl = showEntity.BackdropUrl,
                Seasons = showEntity.Seasons.Select(season => new SeasonDTO
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
            return showDAO;
        }

    }
}
