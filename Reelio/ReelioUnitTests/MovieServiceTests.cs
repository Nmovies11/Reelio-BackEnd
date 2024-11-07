using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BLL.Interfaces.Repositories;
using BLL.Services;
using Common.DTO;
using Common.Entities;

namespace ReelioUnitTests
{
    [TestClass]
    public class MovieServiceTests
    {

        private Mock<IMovieRepository> movieRepositoryMock;
        private MovieService movieService;

        public MovieServiceTests()
        {
            movieRepositoryMock = new Mock<IMovieRepository>();
            movieService = new MovieService(movieRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetRecentMovie_ShouldReturnListOfMovies_WhenMoviesExist()
        {
            var movieDTOs = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Movie 1",
                    Director = "Director 1",
                    ImageUrl = "Image URL 1"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Movie 2",
                    Director = "Director 2",
                    ImageUrl = "Image URL 2"
                }
            };

            movieRepositoryMock.Setup(x => x.GetRecentMovie()).ReturnsAsync(movieDTOs);

            //Act
            var result = await movieService.GetRecentMovie();   

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Movie 1", result[0].Title);
        }

        [TestMethod]
        public async Task GetMovieById_ShouldReturnMovie_WhenMovieExists()
        {
            var movieDTO = new Movie
            {
                Id = 1,
                Title = "Movie 1",
                Description = "Description 1",
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                Director = "Director 1",
                ImageUrl = "Image URL 1",
                BackdropUrl = "Backdrop URL 1"
            };

            movieRepositoryMock.Setup(x => x.GetMovieById(1)).ReturnsAsync(movieDTO);

            //Act
            var result = await movieService.GetMovieById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Movie 1", result.Title);
        }
    }
}
