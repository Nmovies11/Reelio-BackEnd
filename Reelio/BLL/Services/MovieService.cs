using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models.Actor;
using BLL.Models.Movie;
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
        public async Task<List<MovieDAO>> GetRecentMovie()
        {
            List<MovieDTO> movieDTOs = await movieAPIRepository.GetRecentMovie();

            List<MovieDAO> movieDAOs = new List<MovieDAO>();

            foreach (var movieDTO in movieDTOs)
            {
                MovieDAO movieDAO = new MovieDAO
                {
                    Id = movieDTO.Id,
                    Title = movieDTO.Title,
                    Description = movieDTO.Description,
                    ReleaseDate = movieDTO.ReleaseDate,
                    Director = movieDTO.Director,
                    ImageUrl = movieDTO.ImageUrl,
                };

                movieDAOs.Add(movieDAO);
            }

            return movieDAOs;
        }

        public List<ActorDAO> ConvertActors(ICollection<ActorDTO> actorDTOs)
        {
            return actorDTOs.Select(actorDTO => new ActorDAO
            {
                Id = actorDTO.Id,
                Name = actorDTO.Name,
                BirthDate = actorDTO.BirthDate,
                Bio = actorDTO.Bio,
                ImageUrl = actorDTO.ImageUrl
            }).ToList();
        }

        //get movie by ID
        public async Task<MovieDAO> GetMovieById(int id)
        {
            MovieDTO movieDTO = await movieAPIRepository.GetMovieById(id);

            MovieDAO movieDAO = new MovieDAO
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                ReleaseDate = movieDTO.ReleaseDate,
                Director = movieDTO.Director,
                ImageUrl = movieDTO.ImageUrl,
                BackdropUrl = movieDTO.BackdropUrl,
                Actors = ConvertActors(movieDTO.Actors)
            };

            return movieDAO;
        }
    }
}
