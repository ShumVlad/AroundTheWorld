using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Backend;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using AutoMapper;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using NUnit.Framework.Internal;

namespace AroundTheWorld_Tests
{
    [TestClass]
    public class LocationServriceTests
    {
        private LocationService _service;
        private Mock<IRepository<Location>> _mockLocationRepository;
        private Mock<IMapper> _mockMapper;
        private UnitOfWork _unitOfWork;

        [TestInitialize]
        public void Setup()
        {
            _mockLocationRepository = new Mock<IRepository<Location>>();
            _mockMapper = new Mock<IMapper>();

            _unitOfWork = new UnitOfWork(
                groupRepository: null,
                mediaRepository: null,
                placeRepository: null,
                userRepository: null,
                routeRepository: null,
                sensorRepository: null,
                reviewRepository: null,
                positionRepository: null,
                locationRepository: _mockLocationRepository.Object,
                rentItemRepository: null,
                locationRouteRepository: null,
                userGroupRepository: null,
                companyRepository: null,
                context: null
            );

            _service = new LocationService(_mockMapper.Object, _unitOfWork);
        }

        [TestMethod]
        public async Task Update_LocationIsNull_ReturnsFalse()
        {
            var result = await _service.Update(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Update_ValidLocation_ReturnsTrue()
        {
            var location = new Location { Id = "19c20d5e-4025-4a68-9a94-e3ba9c5ef2e9", Name = "Test Location", Address = "address", Description = "descriprion test", ImageUrl = "test", Latitude = 51.5310850344172, Longitude = 9.89768836205061, Type = "Museum" };
            _mockLocationRepository.Setup(r => r.Update(It.IsAny<Location>())).Returns(Task.CompletedTask);

            var result = await _service.Update(location);

            _mockLocationRepository.Verify(r => r.Update(location), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Create_LocationDTOIsNull_ReturnsFalse()
        {
            var result = await _service.Create(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Create_ValidLocationDTO_ReturnsTrue()
        {
            var locationDTO = new LocationDTO { Name = "Test Location" };
            var location = new Location { Id = "1", Name = "Test Location" };

            _mockMapper.Setup(m => m.Map<Location>(locationDTO)).Returns(location);
            _mockLocationRepository.Setup(r => r.Add(It.IsAny<Location>())).Returns(Task.CompletedTask);

            var result = await _service.Create(locationDTO);

            _mockLocationRepository.Verify(r => r.Add(location), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Delete_IdIsNull_ReturnsFalse()
        {
            var result = await _service.Delete(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Delete_ValidId_ReturnsTrue()
        {
            var id = "1";
            _mockLocationRepository.Setup(r => r.Delete(It.IsAny<string>())).Returns(Task.CompletedTask);

            var result = await _service.Delete(id);

            _mockLocationRepository.Verify(r => r.Delete(id), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Get_ValidId_ReturnsLocation()
        {
            var id = "1";
            var location = new Location { Id = id, Name = "Test Location" };
            _mockLocationRepository.Setup(r => r.Get(id)).ReturnsAsync(location);

            var result = await _service.Get(id);

            _mockLocationRepository.Verify(r => r.Get(id), Times.Once);
            Assert.AreEqual(location, result);
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllLocations()
        {
            var locations = new List<Location> { new Location { Id = "1", Name = "Location 1" } };
            _mockLocationRepository.Setup(r => r.GetAll()).ReturnsAsync(locations);

            var result = await _service.GetAll();

            _mockLocationRepository.Verify(r => r.GetAll(), Times.Once);
            Assert.AreEqual(locations, result);
        }

        [TestMethod]
        public async Task GetPaginatedLocations_ReturnsPaginatedLocations()
        {
            var locations = new List<Location> { new Location { Id = "1", Name = "Location 1" } };
            _mockLocationRepository.Setup(r => r.GetPaginated(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(locations);

            var result = await _service.GetPaginatedLocations(1, 10);

            _mockLocationRepository.Verify(r => r.GetPaginated(1, 10), Times.Once);
            Assert.AreEqual(locations, result);
        }
    }
}
