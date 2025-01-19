using BLL.Interfaces.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("shows")]
    public class ShowController : Controller
    {
        private readonly IShowService showService;
        public ShowController(IShowService _showService)
        {
            showService = _showService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ShowDTO>))]
        public async Task<PaginatedList<ShowDTO>> GetShows(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchQuery = null,
            [FromQuery] string? genre = null)
        {
            return await showService.GetShows(pageNumber, pageSize, searchQuery, genre);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShowDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ShowDTO> GetShowById(int id)
        {
            return await showService.GetShowById(id);
        }

    }
}
