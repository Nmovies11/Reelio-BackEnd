using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.DTO;

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

            var showDTOs = await _showRepository.GetRecentShows();

            if(showDTOs == null)
            {
                return null;
            }

            return showDTOs;
        }

        public async Task<ShowDTO> GetShowById(int id)
        {
            var showDTO = await _showRepository.GetShowById(id);

            if(showDTO == null)
            {
                return null;
            }

            return showDTO;
        }

    }
}
