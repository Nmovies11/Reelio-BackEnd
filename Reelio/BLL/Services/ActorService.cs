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
    public class ActorService : IActorService
    {
        private readonly IActorRepository actorAPIRepository;

        public ActorService(IActorRepository iactorRepository)
        {
            actorAPIRepository = iactorRepository;
        }

        public Task<PaginatedList<ActorDTO>> GetActors(int pageNumber, int pageSize, string? searchQuery)
        {
            return actorAPIRepository.GetActors(pageNumber, pageSize, searchQuery);
        }

        public Task<ActorDTODetails> GetActorById(int id)
        {
            return actorAPIRepository.GetActorById(id);
        }
    }
}
