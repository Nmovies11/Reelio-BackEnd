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
using DAL.API.Entities;


namespace ReelioUnitTests
{
    [TestClass]
    public class MovieServiceTests
    {
        private Mock<IMovieRepository> _mockRepository;
        private MovieService _movieService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IMovieRepository>();
            _movieService = new MovieService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetRecentMovie_ShouldReturnRecentMovies()
        {
            // Arrange
            var mockMovies = new List<MovieDTO>
            {
                new MovieDTO { Id = 1, Title = "Movie 1", ReleaseDate = DateOnly.FromDateTime(DateTime.Now), Director = "Director 1", ImageUrl = "url1" },
                new MovieDTO { Id = 2, Title = "Movie 2", ReleaseDate = DateOnly.FromDateTime(DateTime.Now), Director = "Director 2", ImageUrl = "url2" }
            };

            _mockRepository
                .Setup(repo => repo.GetRecentMovies())
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _movieService.GetRecentMovies();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Movie 1", result[0].Title);
        }

        [TestMethod]
        public async Task GetMovieById_ShouldReturnMovieDetails()
        {
            // Arrange
            var mockMovie = new MovieDTODetails
            {
                Id = 1,
                Title = "Movie 1",
                Description = "A great movie",
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                Director = "Director 1",
                ImageUrl = "url1",
                BackdropUrl = "backdrop1",
                Actors = new List<ActorDTO>
                {
                    new ActorDTO { Id = 1, Name = "Actor 1", Role = "Role 1", ImageUrl = "actorUrl1" },
                    new ActorDTO { Id = 2, Name = "Actor 2", Role = "Role 2", ImageUrl = "actorUrl2" }
                }
            };

            _mockRepository
                .Setup(repo => repo.GetMovieById(1))
                .ReturnsAsync(mockMovie);

            // Act
            var result = await _movieService.GetMovieById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Movie 1", result.Title);
            Assert.AreEqual(2, result.Actors.Count);
        }


    }

}
