using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.DTO;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieAPIRepository;

        public MovieService(IMovieRepository imovieAPIRepository)
        {
            movieAPIRepository = imovieAPIRepository;
        }

        //get recent movie
        public async Task<List<MovieDTO>> GetRecentMovie()
        {
            List<Movie> movieEntities  = await movieAPIRepository.GetRecentMovie();

            List<MovieDTO> movieDTOs = new List<MovieDTO>();

            foreach (var movieEntity in movieEntities )
            {
                MovieDTO movieDAO = new MovieDTO
                {
                    Id = movieEntity.Id,
                    Title = movieEntity.Title,
                    ReleaseDate = movieEntity.ReleaseDate,
                    Director = movieEntity.Director,
                    ImageUrl = movieEntity.ImageUrl,
                };

                movieDTOs.Add(movieDAO);
            }

            return movieDTOs;
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

        //get movie by ID
        public async Task<MovieDTODetails> GetMovieById(int id)
        {
            Movie MovieEntity = await movieAPIRepository.GetMovieById(id);

            MovieDTODetails movieDAO = new MovieDTODetails
            {
                Id = MovieEntity.Id,
                Title = MovieEntity.Title,
                Description = MovieEntity.Description,
                ReleaseDate = MovieEntity.ReleaseDate,
                Director = MovieEntity.Director,
                ImageUrl = MovieEntity.ImageUrl,
                BackdropUrl = MovieEntity.BackdropUrl,
                Actors = ConvertActors(MovieEntity.Actors)
            };

            return movieDAO;
        }
    }
}
