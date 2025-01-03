using Microsoft.EntityFrameworkCore;
using ReservationApp.Api.Infraestructure;
using ReservationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApp.Tests
{
    public class UserRepositoryTests : TestBase
    {
        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            // Act
            await repository.AddAsync(user);

            // Assert
            var result = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            Assert.NotNull(result);
            Assert.Equal(user.FullName, result.FullName);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Jane Smith",
                Email = "janesmith@example.com",
                PhoneNumber = "0987654321",
                IsActive = true
            };
            await repository.AddAsync(user);

            // Act
            var result = await repository.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FullName, result.FullName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new UserRepository(context);

            var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FullName = "User One",
                Email = "userone@example.com",
                PhoneNumber = "1111111111",
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                FullName = "User Two",
                Email = "usertwo@example.com",
                PhoneNumber = "2222222222",
                IsActive = true
            }
        };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(users.Count, result.Count());
            Assert.Contains(result, u => u.FullName == "User One");
            Assert.Contains(result, u => u.FullName == "User Two");
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingUser()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Initial Name",
                Email = "initial@example.com",
                PhoneNumber = "1234567890",
                IsActive = true
            };
            await repository.AddAsync(user);

            // Act
            user.FullName = "Updated Name";
            user.Email = "updated@example.com";
            await repository.UpdateAsync(user);

            // Assert
            var result = await repository.GetByIdAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.FullName);
            Assert.Equal("updated@example.com", result.Email);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "To Be Deleted",
                Email = "tobedeleted@example.com",
                PhoneNumber = "1234567890",
                IsActive = true
            };
            await repository.AddAsync(user);

            // Act
            await repository.DeleteAsync(user.Id);

            // Assert
            var result = await repository.GetByIdAsync(user.Id);
            Assert.Null(result);
        }
    }

}
