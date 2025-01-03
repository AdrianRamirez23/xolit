using Microsoft.AspNetCore.Mvc;
using Moq;
using ReservationApp.Api.Controllers;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApp.Tests
{
    public class SpacesControllerTests
    {
        [Fact]
        public async Task GetSpaces_ReturnsOkResultWithSpaces()
        {
            // Arrange
            var spaces = new List<Space>
            {
                new Space { Id = Guid.NewGuid(), Name = "Room A", Description = "Conference room" },
                new Space { Id = Guid.NewGuid(), Name = "Room B", Description = "Meeting room" }
            };

            var mockService = new Mock<ISpaceRepository>();
            mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(spaces);

            var controller = new SpacesController(mockService.Object);

            // Act
            var result = await controller.GetSpaces();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSpaces = Assert.IsType<List<Space>>(okResult.Value);
            Assert.Equal(spaces.Count, returnedSpaces.Count);
        }

        [Fact]
        public async Task AddSpace_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockRepository = new Mock<ISpaceRepository>();
            var controller = new SpacesController(mockRepository.Object);
            var space = new Space { Name = "Room A", Description = "Conference Room", Capacity = 20, IsActive = true };

            // Act
            var result = await controller.AddSpace(space);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedSpace = Assert.IsType<Space>(createdAtActionResult.Value);
            Assert.Equal(space.Name, returnedSpace.Name);
        }
    }
}
