using BLL.Interfaces.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("showsx")]
    public class ShowController : Controller
    {
        private readonly IShowService showService;
        public ShowController(IShowService _showService)
        {
            showService = _showService;
        }

        [HttpGet("recentshows")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShowDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<ShowDTO>> GetRecentShows()
        {
            return await showService.GetRecentShows();
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
