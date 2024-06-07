using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Backend.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Stripe.Terminal;

namespace AroundTheWorld_Tests
{
    [TestClass]
    public class LocationServiceTests
    {
        private LocationService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ILocationRepository> _mockLocationRepository;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<UnitOfWork>();
            _mockLocationRepository = new Mock<ILocationRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork.Setup(u => u.LocationRepository).Returns(_mockLocationRepository.Object);

            _service = new LocationService(_mockMapper.Object, _mockUnitOfWork.Object);
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
            var location = new Location { Id = "1", Name = "Test Location" };
            _mockLocationRepository.Setup(r => r.Update(It.IsAny<Location>())).Returns(Task.CompletedTask);

            var result = await _service.Update(location);

            _mockLocationRepository.Verify(r => r.Update(location), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
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
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
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
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
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
