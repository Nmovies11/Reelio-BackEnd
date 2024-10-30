using BLL.Interfaces.Services;
using BLL.Models.Show;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowController : Controller
    {
        private readonly IShowService showService;
        public ShowController(IShowService _showService)
        {
            showService = _showService;
        }

        [HttpGet("recentshows")]
        public async Task<List<ShowDAO>> GetRecentShows()
        {
            return await showService.GetRecentShows();
        }

        [HttpGet("{id}")]
        public async Task<ShowDAO> GetShowById(int id)
        {
            return await showService.GetShowById(id);
        }

    }
}
