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
    public class UsersControllerTests
    {
        [Fact]
        public async Task CreateUser_ReturnsCreatedAtActionResult_WhenValidUser()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UsersController(mockRepository.Object);

            var newUser = new User
            {
                FullName = "John Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            // Act
            var result = await controller.CreateUser(newUser);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);

            Assert.Equal(newUser.FullName, returnedUser.FullName);
            Assert.Equal(newUser.Email, returnedUser.Email);
            Assert.Equal(newUser.PhoneNumber, returnedUser.PhoneNumber);

            // Verificar que el repositorio fue llamado
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenUserIsNull()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UsersController(mockRepository.Object);

            // Act
            var result = await controller.CreateUser(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenFullNameOrEmailIsEmpty()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UsersController(mockRepository.Object);

            var invalidUser = new User
            {
                FullName = "",
                Email = "",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            // Act
            var result = await controller.CreateUser(invalidUser);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
