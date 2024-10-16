using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieAPIService : IMovieAPIService
    {
        private readonly IMovieAPIRepository movieAPIRepository;

        public MovieAPIService(IMovieAPIRepository imovieAPIRepository)
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
                    ImageUrl = movieDTO.ImageUrl
                };

                movieDAOs.Add(movieDAO);
            }

            return movieDAOs;
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
                BackdropUrl = movieDTO.BackdropUrl
            };

            return movieDAO;
        }
    }
}
