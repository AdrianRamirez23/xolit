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
    public class ReservationRepositoryTests : TestBase
    {
        [Fact]
        public async Task AddReservationAsync_ShouldAddReservation()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new ReservationRepository(context);
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };

            // Act
            await repository.AddReservationAsync(reservation);

            // Assert
            var result = await context.Reservations.FirstOrDefaultAsync(r => r.Id == reservation.Id);
            Assert.NotNull(result);
            Assert.Equal(reservation.Id, result.Id);
        }

        [Fact]
        public async Task GetReservationsAsync_ShouldReturnReservations()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new ReservationRepository(context);

            var space = new Space
            {
                Id = Guid.NewGuid(),
                Name = "Conference Room",
                Description = "Room for meetings",
                Capacity = 10,
                IsActive = true
            };
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                IsActive = true
            };
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = space.Id,
                UserId = user.Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                Space = space,
                User = user
            };

            // Agregar entidades relacionadas al contexto
            context.Spaces.Add(space);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Agregar la reserva
            await repository.AddReservationAsync(reservation);

            // Act
            var result = await repository.GetReservationsAsync(null, null, null, null);

            // Assert
            Assert.Single(result);
            var returnedReservation = result.First();
            Assert.Equal(reservation.Id, returnedReservation.Id);
            Assert.Equal(reservation.SpaceId, returnedReservation.SpaceId);
            Assert.Equal(reservation.UserId, returnedReservation.UserId);
            Assert.Equal(space.Name, returnedReservation.Space.Name);
            Assert.Equal(user.FullName, returnedReservation.User.FullName);
        }

    }

}
