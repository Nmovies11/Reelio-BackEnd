using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IActorService
    {
        public Task<PaginatedList<ActorDTO>> GetActors(int pageNumber, int pageSize, string? searchQuery);
        public Task<ActorDTODetails> GetActorById(int id);
    }
}
