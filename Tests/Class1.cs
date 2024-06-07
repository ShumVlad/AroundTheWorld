using Microsoft.VisualStudio.TestTools.UnitTesting;
using AroundTheWorld.Controllers; // Додайте простір імен вашого контролера
using AroundTheWorld.Models;      // Якщо необхідно, додайте інші простори імен
using Moq;

namespace AroundTheWorld_Tests
{
    [TestClass]
    public class LocationControllerTests
    {
        private LocationController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Мок об'єктів, необхідних для контролера (наприклад, сервісів)
            var mockLocationService = new Mock<ILocationService>();
            _controller = new LocationController(mockLocationService.Object);
        }

        [TestMethod]
        public void DeleteLocation_StringId_ReturnsTrue()
        {
            // Arrange
            string id = "19c20d5e-4025-4a68-9a94-e3ba9c5ef2e9";
            var mockLocationService = new Mock<ILocationService>();
            mockLocationService.Setup(service => service.DeleteLocation(id)).Returns(true);

            _controller = new LocationController(mockLocationService.Object);

            // Act
            var result = _controller.DeleteLocation(id);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
