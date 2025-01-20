using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DAL.API.Entities;

using BLL.Interfaces.Repositories;
using BLL.Services;
using Common.DTO;
using BLL.Interfaces.Services;

namespace ReelioUnitTests
{
    [TestClass]
    public class ShowServiceTests
    {
        private Mock<IShowRepository> _mockShowRepository;
        private IShowService _showService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockShowRepository = new Mock<IShowRepository>();
            _showService = new ShowService(_mockShowRepository.Object);
        }

        [TestMethod]
        public async Task GetShowById_ShouldReturnShow_WhenShowExists()
        {
            // Arrange
            var showId = 1;
            var expectedShow = new ShowDTO { Id = showId, Title = "Test Show" };
            _mockShowRepository.Setup(repo => repo.GetShowById(showId))
                .ReturnsAsync(expectedShow);

            // Act
            var result = await _showService.GetShowById(showId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedShow.Id, result.Id);
            Assert.AreEqual(expectedShow.Title, result.Title);
            _mockShowRepository.Verify(repo => repo.GetShowById(showId), Times.Once);
        }

        [TestMethod]
        public async Task GetShowById_ShouldReturnNull_WhenShowDoesNotExist()
        {
            // Arrange
            var showId = 1;
            _mockShowRepository.Setup(repo => repo.GetShowById(showId))
                .ReturnsAsync((ShowDTO)null);

            // Act
            var result = await _showService.GetShowById(showId);

            // Assert
            Assert.IsNull(result);
            _mockShowRepository.Verify(repo => repo.GetShowById(showId), Times.Once);
        }

        [TestMethod]
        public async Task GetShows_ShouldReturnPaginatedList_WhenShowsExist()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var searchQuery = "Test";
            var genre = "Comedy";

            var expectedShows = new PaginatedList<ShowDTO>(
                new List<ShowDTO>
                {
                    new ShowDTO { Id = 1, Title = "Test Show 1" },
                    new ShowDTO { Id = 2, Title = "Test Show 2" }
                },
                totalCount: 2,
                pageNumber: pageNumber,
                pageSize: pageSize
            );


            _mockShowRepository.Setup(repo => repo.GetShows(pageNumber, pageSize, searchQuery, genre))
                .ReturnsAsync(expectedShows);

            // Act
            var result = await _showService.GetShows(pageNumber, pageSize, searchQuery, genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(pageNumber, result.PageNumber);
            Assert.AreEqual(pageSize, result.PageSize);
            _mockShowRepository.Verify(repo => repo.GetShows(pageNumber, pageSize, searchQuery, genre), Times.Once);
        }

        [TestMethod]
        public async Task GetShows_ShouldReturnNull_WhenNoShowsExist()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var searchQuery = "Test";
            var genre = "Comedy";

            _mockShowRepository.Setup(repo => repo.GetShows(pageNumber, pageSize, searchQuery, genre))
                .ReturnsAsync((PaginatedList<ShowDTO>)null);

            // Act
            var result = await _showService.GetShows(pageNumber, pageSize, searchQuery, genre);

            // Assert
            Assert.IsNull(result);
            _mockShowRepository.Verify(repo => repo.GetShows(pageNumber, pageSize, searchQuery, genre), Times.Once);
        }
    }

}
