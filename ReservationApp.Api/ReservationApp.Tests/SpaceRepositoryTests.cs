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
    public class SpaceRepositoryTests : TestBase
    {
        [Fact]
        public async Task AddAsync_ShouldAddSpace()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new SpaceRepository(context);

            var space = new Space
            {
                Id = Guid.NewGuid(),
                Name = "Conference Room",
                IsActive = true,
                Capacity = 100,
                Description = "Description",
            };

            // Act
            await repository.AddAsync(space);

            // Assert
            var result = await context.Spaces.FirstOrDefaultAsync(s => s.Id == space.Id);
            Assert.NotNull(result);
            Assert.Equal(space.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSpace()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new SpaceRepository(context);

            var space = new Space
            {
                Id = Guid.NewGuid(),
                Name = "Conference Room",
                IsActive = true,
                Capacity = 100,
                Description = "Description",
            };
            await repository.AddAsync(space);

            // Act
            var result = await repository.GetByIdAsync(space.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(space.Name, result.Name);
        }
    }
}
