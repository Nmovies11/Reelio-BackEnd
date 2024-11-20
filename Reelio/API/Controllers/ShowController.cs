using BLL.Interfaces.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("show")]
    public class ShowController : Controller
    {
        private readonly IShowService showService;
        public ShowController(IShowService _showService)
        {
            showService = _showService;
        }

        [HttpGet("recentshows")]
        public async Task<List<ShowDTO>> GetRecentShows()
        {
            return await showService.GetRecentShows();
        }

        [HttpGet("{id}")]
        public async Task<ShowDTO> GetShowById(int id)
        {
            return await showService.GetShowById(id);
        }

    }
}
