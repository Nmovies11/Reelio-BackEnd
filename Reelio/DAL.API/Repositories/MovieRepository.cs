using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using Common.Entities;
using Microsoft.Extensions.Configuration;

namespace DAL.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<Movie>> GetRecentMovie()
        {
            Uri url = new Uri("https://localhost:7076/movie/recentmovies");
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            //turn content into a list of MovieDTO

            var movies = JsonSerializer.Deserialize<List<Movie>>(content);

            return movies;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            Uri url = new Uri("https://localhost:7076/movie/" + id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            //turn content into a MovieDTO

            var movie = JsonSerializer.Deserialize<Movie>(content);

            return movie;
        }
    }
}