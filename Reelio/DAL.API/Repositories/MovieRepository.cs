using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using Common.Entities;
using Common.DTO;
using Microsoft.Extensions.Configuration;
using DAL.API.Entities;

namespace DAL.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<MovieDTO>> GetRecentMovies()
        {
            Uri url = new Uri("https://localhost:7076/movie/recentmovies");
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            // Deserialize content into a list of Movie entities
            var movieEntities = JsonSerializer.Deserialize<List<Movie>>(content);

            if (movieEntities == null || !movieEntities.Any())
            {
                return new List<MovieDTO>(); // Return an empty list if no movies found
            }

            // Convert entities to DTOs
            return movieEntities.Select(movieEntity => new MovieDTO
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                ReleaseDate = movieEntity.ReleaseDate,
                Director = movieEntity.Director,
                ImageUrl = movieEntity.ImageUrl
            }).ToList();
        }

        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            Uri url = new Uri("https://localhost:7076/movie/" + id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var movieEntity = JsonSerializer.Deserialize<Movie>(content);

            if (movieEntity == null)
            {
                throw new InvalidOperationException("Failed to deserialize movie.");
            }

            // Convert the Movie entity into a MovieDTODetails and return it
            var movieDTO = new MovieDTODetails
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                Description = movieEntity.Description,
                ReleaseDate = movieEntity.ReleaseDate,
                Director = movieEntity.Director,
                ImageUrl = movieEntity.ImageUrl,
                BackdropUrl = movieEntity.BackdropUrl,
                Actors = ConvertActors(movieEntity.Actors)
            };

            return movieDTO;
        }

        public List<ActorDTO> ConvertActors(ICollection<Actor> actorDTOs)
        {
            if (actorDTOs == null)
            {
                return new List<ActorDTO>(); // or return an empty list as fallback
            }

            return actorDTOs.Select(actorDTO => new ActorDTO
            {
                Id = actorDTO.Id,
                Name = actorDTO.Name,
                ImageUrl = actorDTO.ImageUrl,
                Role = actorDTO.Role
            }).ToList();
        }

    }
}