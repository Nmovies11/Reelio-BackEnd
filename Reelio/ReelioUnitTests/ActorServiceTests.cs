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
        public class ActorServiceTests
        {
            private Mock<IActorRepository> mockActorRepository;
            private IActorService actorService;

            [TestInitialize]
            public void Setup()
            {
                // Arrange: Create the mock of IActorRepository
                mockActorRepository = new Mock<IActorRepository>();

                // Initialize ActorService with the mocked repository
                actorService = new ActorService(mockActorRepository.Object);
            }

            [TestMethod]
            public async Task GetActors_ShouldReturnPaginatedListOfActorDTO_WhenCalled()
            {
                // Arrange
                int pageNumber = 1;
                int pageSize = 10;
                string searchQuery = "test";
                var expectedActors = new PaginatedList<ActorDTO>(
                    new List<ActorDTO>
                    {
                        new ActorDTO { Id = 1, Name = "Actor 1" },
                        new ActorDTO { Id = 2, Name = "Actor 2" }
                    },
                    2,   
                    pageNumber,  
                    pageSize  
                );

                mockActorRepository.Setup(repo => repo.GetActors(pageNumber, pageSize, searchQuery))
                    .ReturnsAsync(expectedActors);

                // Act
                var result = await actorService.GetActors(pageNumber, pageSize, searchQuery);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Items.Count);
                Assert.AreEqual("Actor 1", result.Items[0].Name);
                Assert.AreEqual("Actor 2", result.Items[1].Name);
            }

            [TestMethod]
            public async Task GetActorById_ShouldReturnActorDTODetails_WhenActorExists()
            {
                // Arrange
                int actorId = 1;
                var expectedActorDetails = new ActorDTODetails
                {
                    Id = 1,
                    Name = "Actor 1",
                    Bio = "Actor 1 biography"
                };

                mockActorRepository.Setup(repo => repo.GetActorById(actorId))
                    .ReturnsAsync(expectedActorDetails);

                // Act
                var result = await actorService.GetActorById(actorId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("Actor 1", result.Name);
                Assert.AreEqual("Actor 1 biography", result.Bio);
            }

            [TestMethod]
            public async Task GetActorById_ShouldReturnNull_WhenActorDoesNotExist()
            {
                // Arrange
                int actorId = 99;
                mockActorRepository.Setup(repo => repo.GetActorById(actorId))
                    .ReturnsAsync((ActorDTODetails)null);

                // Act
                var result = await actorService.GetActorById(actorId);

                // Assert
                Assert.IsNull(result);
            }

            [TestMethod]
            public async Task GetActors_ShouldReturnEmptyList_WhenNoActorsFound()
            {
                // Arrange
                int pageNumber = 1;
                int pageSize = 10;
                string searchQuery = "nonexistent";

                var expectedActors = new PaginatedList<ActorDTO>(

                    new List<ActorDTO> { 
                    
                    },
                    totalCount: 2,
                    pageNumber: pageNumber,
                    pageSize: pageSize
                );

                mockActorRepository.Setup(repo => repo.GetActors(pageNumber, pageSize, searchQuery))
                    .ReturnsAsync(expectedActors);

                // Act
                var result = await actorService.GetActors(pageNumber, pageSize, searchQuery);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Items.Count);
            }
        }
    }

