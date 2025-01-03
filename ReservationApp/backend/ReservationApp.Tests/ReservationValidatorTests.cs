using FluentValidation;
using Moq;
using ReservationApp.Domain.Core.Validators;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Tests
{
    public class ReservationValidatorTests
    {
        private readonly IValidator<Reservation> _validator;

        public ReservationValidatorTests()
        {
            var mockReservationRepo = new Mock<IReservationRepository>();
            var mockSpaceRepo = new Mock<ISpaceRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            mockSpaceRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Space { Id = Guid.NewGuid(), IsActive = true });
            mockUserRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new User { Id = Guid.NewGuid(), IsActive = true });

            _validator = new ReservationValidator(mockReservationRepo.Object, mockSpaceRepo.Object, mockUserRepo.Object);
        }

        [Fact]
        public async Task ValidateAsync_ShouldPass_WhenValidReservation()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };

            // Act
            var result = await _validator.ValidateAsync(reservation);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ValidateAsync_ShouldFail_WhenStartDateAfterEndDate()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now
            };

            // Act
            var result = await _validator.ValidateAsync(reservation);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "La fecha de inicio debe ser antes de la fecha de fin.");
        }
    }

}
