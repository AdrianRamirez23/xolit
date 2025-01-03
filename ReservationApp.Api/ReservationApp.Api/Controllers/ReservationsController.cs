using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IValidator<Reservation> _validator;

        public ReservationsController(
            IReservationRepository reservationRepository,
            IValidator<Reservation> validator)
        {
            _reservationRepository = reservationRepository;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
        {
            // Validar los datos de la reserva
            var validationResult = await _validator.ValidateAsync(reservation);

            if (!validationResult.IsValid)
            {
                // Retornar errores de validación
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            // Procesar la reserva si es válida
            await _reservationRepository.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservations), new { id = reservation.Id }, reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            await _reservationRepository.RemoveReservationAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] Guid? spaceId, [FromQuery] Guid? userId)
        {
            var reservations = await _reservationRepository.GetReservationsAsync(startDate, endDate, spaceId, userId);


           
            return Ok(reservations.ToList());
        }

    }
}
