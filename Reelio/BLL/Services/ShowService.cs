using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models.Show;

namespace BLL.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public async Task<List<ShowDAO>> GetRecentShows()
        {

            var shows = await _showRepository.GetRecentShows();

            List<ShowDAO> showDAOs = new List<ShowDAO>();
            foreach (var show in shows)
            {
                ShowDAO showDAO = new ShowDAO
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

        public async Task<ShowDAO> GetShowById(int id)
        {
            var show = await _showRepository.GetShowById(id);
            ShowDAO showDAO = new ShowDAO
            {
                Id = show.Id,
                Title = show.Title,
                Description = show.Description,
                ReleaseDate = show.ReleaseDate,
                ImageUrl = show.ImageUrl,
                BackdropUrl = show.BackdropUrl,
            };
            return showDAO;
        }

    }
}
