using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using BLL.Models;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public MovieRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        private readonly HttpClient _client = new HttpClient();

        public async Task<List<MovieDTO>> GetRecentMovie()
        {
            Uri url = new Uri("https://localhost:7076/movie/recentmovies");
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            //turn content into a list of MovieDTO

            var movies = JsonSerializer.Deserialize<List<MovieDTO>>(content);

            return movies;
        }

        public async Task<MovieDTO> GetMovieById(int id)
        {
            Uri url = new Uri("https://localhost:7076/movie/" + id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            //turn content into a MovieDTO

            var movie = JsonSerializer.Deserialize<MovieDTO>(content);

            return movie;
        }
    }
}