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
using BLL.Interfaces.Services;


namespace ReelioUnitTests
{
    [TestClass]
    public class MovieServiceTests
    {
        private Mock<IMovieRepository> _mockMovieRepository;
        private IMovieService _movieService;

        [TestInitialize]
        public void SetUp()
        {
            // Initialize the mock repository
            _mockMovieRepository = new Mock<IMovieRepository>();

            // Inject the mock repository into the MovieService
            _movieService = new MovieService(_mockMovieRepository.Object);
        }

        [TestMethod]
        public async Task GetRecentMovies_ShouldReturnListOfMovieDTOs()
        {
            // Arrange
            var expectedMovies = new List<MovieDTO>
            {
                new MovieDTO { Id = 1, Title = "Movie 1", Genre = "Action" },
                new MovieDTO { Id = 2, Title = "Movie 2", Genre = "Drama" }
            };

            _mockMovieRepository.Setup(repo => repo.GetRecentMovies()).ReturnsAsync(expectedMovies);

            // Act
            var result = await _movieService.GetRecentMovies();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMovies.Count, result.Count);
            Assert.AreEqual(expectedMovies[0].Title, result[0].Title);
        }

        [TestMethod]
        public async Task GetMovieById_ShouldReturnMovieDTODetails()
        {
            // Arrange
            var movieId = 1;
            var expectedMovieDetails = new MovieDTODetails
            {
                Id = 1,
                Title = "Movie 1",
                Genre = "Action",
                Description = "An exciting action movie"
            };

            _mockMovieRepository.Setup(repo => repo.GetMovieById(movieId)).ReturnsAsync(expectedMovieDetails);

            // Act
            var result = await _movieService.GetMovieById(movieId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMovieDetails.Title, result.Title);
            Assert.AreEqual(expectedMovieDetails.Description, result.Description);
        }

        [TestMethod]
        public async Task GetMovies_ShouldReturnPaginatedListOfMovies()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string searchQuery = "Action";
            string genre = "Action";

            var expectedMovies = new PaginatedList<MovieDTO>(
                new List<MovieDTO>
                {
                    new MovieDTO { Id = 1, Title = "Test Show 1" },
                    new MovieDTO { Id = 2, Title = "Test Show 2" }
                },
                totalCount: 2,
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            _mockMovieRepository.Setup(repo => repo.GetMovies(pageNumber, pageSize, searchQuery, genre))
                .ReturnsAsync(expectedMovies);

            // Act
            var result = await _movieService.GetMovies(pageNumber, pageSize, searchQuery, genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMovies.Items.Count, result.Items.Count);
            Assert.AreEqual(expectedMovies.TotalCount, result.TotalCount);
        }
    }
}

