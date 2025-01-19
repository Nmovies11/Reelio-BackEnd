using BLL.Interfaces.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("actors")]
    public class ActorController : Controller
    {

        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MovieDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index(
        int pageNumber = 1,
        int pageSize = 10,
        string? searchQuery = null)
        {
            var paginatedActors = await _actorService.GetActors(pageNumber, pageSize, searchQuery);

            return Ok(paginatedActors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDTODetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActorById(int id)
        {
            ActorDTODetails actor = await _actorService.GetActorById(id);
            return Ok(actor);
        }
    }
}
