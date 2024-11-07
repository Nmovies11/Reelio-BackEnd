 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace BLL.Interfaces.Services
{
    public interface IMovieService
    {
        public Task<List<MovieDTO>> GetRecentMovie();
        public Task<MovieDTODetails> GetMovieById(int id);
        public List<ActorDTO> ConvertActors(ICollection<Actor> actorDTOs);

    }
}
