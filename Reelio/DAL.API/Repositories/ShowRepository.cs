using BLL.Interfaces.Repositories;
using Common.DTO;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace DAL.API.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<Show>> GetRecentShows()
        {
            Uri url = new Uri("https://localhost:7076/show/recentshows");
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);


            var shows = JsonSerializer.Deserialize<List<Show>>(content);

            return shows;

        }

        public async Task<Show> GetShowById(int id)
        {
            Uri url = new Uri("https://localhost:7076/show/" + id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var show = JsonSerializer.Deserialize<Show>(content);

            return show;
        }
    }
}
