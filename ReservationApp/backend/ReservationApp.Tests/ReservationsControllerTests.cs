using FluentValidation;
using FluentValidation.Results;
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
    public class ReservationsControllerTests
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IValidator<Reservation>> _validatorMock;
        private readonly ReservationsController _controller;

        public ReservationsControllerTests()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _validatorMock = new Mock<IValidator<Reservation>>();
            _controller = new ReservationsController(
                _reservationRepositoryMock.Object,
                _validatorMock.Object);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(-1) // Fecha inválida
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<Reservation>(), default))
                .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("EndDate", "La fecha de fin debe ser después de la fecha de inicio.") }));

            // Act
            var result = await _controller.CreateReservation(reservation);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsAssignableFrom<IEnumerable<string>>(badRequestResult.Value);
            Assert.Contains("La fecha de fin debe ser después de la fecha de inicio.", errors);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnCreatedAtAction_WhenValid()
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

            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<Reservation>(), default))
                .ReturnsAsync(new ValidationResult());

            _reservationRepositoryMock
                .Setup(r => r.AddReservationAsync(It.IsAny<Reservation>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateReservation(reservation);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdReservation = Assert.IsAssignableFrom<Reservation>(createdResult.Value);
            Assert.Equal(reservation.Id, createdReservation.Id);
        }
        [Fact]
        public async Task CancelReservation_ShouldReturnNoContent_WhenReservationExists()
        {
            // Arrange
            var reservationId = Guid.NewGuid();

            _reservationRepositoryMock
                .Setup(r => r.RemoveReservationAsync(reservationId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CancelReservation(reservationId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CancelReservation_ShouldReturnNoContent_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = Guid.NewGuid();

            _reservationRepositoryMock
                .Setup(r => r.RemoveReservationAsync(reservationId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CancelReservation(reservationId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task GetReservations_ShouldReturnReservations()
        {
            // Arrange
            var reservations = new List<Reservation>
    {
        new Reservation
        {
            Id = Guid.NewGuid(),
            SpaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(1)
        }
    };

            _reservationRepositoryMock
                .Setup(r => r.GetReservationsAsync(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<Guid?>(), It.IsAny<Guid?>()))
                .ReturnsAsync(reservations);

            // Act
            var result = await _controller.GetReservations(null, null, null, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReservations = Assert.IsAssignableFrom<IEnumerable<Reservation>>(okResult.Value);
            Assert.Single(returnedReservations);
            Assert.Equal(reservations.First().Id, returnedReservations.First().Id);
        }

    }
}
