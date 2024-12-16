using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DAL.API.Entities;

using BLL.Interfaces.Repositories;
using BLL.Services;
using Common.DTO;

namespace ShowServiceTests
{
    [TestClass]
    public class ShowServiceTests
    {
        private Mock<IShowRepository> _mockRepository;
        private ShowService _showService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IShowRepository>();
            _showService = new ShowService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetRecentShows_ShouldReturnRecentShows()
        {
            // Arrange
            var mockShows = new List<ShowDTO>
            {
                new ShowDTO { Id = 1, Title = "Show 1", Description = "Description 1", ReleaseDate = DateOnly.FromDateTime(DateTime.Now), ImageUrl = "url1", BackdropUrl = "backdrop1" },
                new ShowDTO { Id = 2, Title = "Show 2", Description = "Description 2", ReleaseDate = DateOnly.FromDateTime(DateTime.Now), ImageUrl = "url2", BackdropUrl = "backdrop2" }
            };

            _mockRepository
                .Setup(repo => repo.GetRecentShows())
                .ReturnsAsync(mockShows);

            // Act
            var result = await _showService.GetRecentShows();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Show 1", result[0].Title);
            Assert.AreEqual("Description 1", result[0].Description);
        }

        [TestMethod]
        public async Task GetShowById_ShouldReturnShowDetailsWithSeasonsAndEpisodes()
        {
            // Arrange
            var mockShow = new ShowDTO
            {
                Id = 1,
                Title = "Show 1",
                Description = "A great show",
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                ImageUrl = "url1",
                BackdropUrl = "backdrop1",
                Seasons = new List<SeasonDTO>
                {
                    new SeasonDTO
                    {
                        Id = 1,
                        SeasonNumber = 1,
                        Description = "Season 1 Description",
                        ReleaseDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1),
                        ShowId = 1,
                        Episodes = new List<EpisodeDTO>
                        {
                            new EpisodeDTO { Id = 1, Title = "Episode 1", Description = "Episode 1 Description", ReleaseDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-12), Director = "Director 1", EpisodeNumber = 1 },
                            new EpisodeDTO { Id = 2, Title = "Episode 2", Description = "Episode 2 Description", ReleaseDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-11), Director = "Director 2", EpisodeNumber = 2 }
                        }
                    }
                }
            };

            _mockRepository
                .Setup(repo => repo.GetShowById(1))
                .ReturnsAsync(mockShow);

            // Act
            var result = await _showService.GetShowById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Show 1", result.Title);
            Assert.AreEqual(1, result.Seasons.Count);

        }

        [TestMethod]
        public async Task GetShowById_ShouldReturnNullWhenShowNotFound()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetShowById(It.IsAny<int>()))
                .ReturnsAsync((ShowDTO)null);

            // Act
            var result = await _showService.GetShowById(999); // Passing non-existent ID

            // Assert
            Assert.IsNull(result);
        }
    }

}
