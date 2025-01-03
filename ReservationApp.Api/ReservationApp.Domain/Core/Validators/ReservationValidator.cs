using FluentValidation;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Domain.Core.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISpaceRepository _spaceRepository;
        private readonly IUserRepository _userRepository;

        public ReservationValidator(
            IReservationRepository reservationRepository,
            ISpaceRepository spaceRepository,
            IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _spaceRepository = spaceRepository;
            _userRepository = userRepository;

            // Validación básica de fechas
            RuleFor(r => r.StartDate)
                .LessThan(r => r.EndDate)
                .WithMessage("La fecha de inicio debe ser antes de la fecha de fin.");

            RuleFor(r => r.EndDate)
                .GreaterThan(r => r.StartDate)
                .WithMessage("La fecha de fin debe ser después de la fecha de inicio.");

            // Validar duración mínima y máxima
            RuleFor(r => (r.EndDate - r.StartDate).TotalMinutes)
                .GreaterThanOrEqualTo(30)
                .WithMessage("La duración mínima de una reserva es de 30 minutos.")
                .LessThanOrEqualTo(480)
                .WithMessage("La duración máxima de una reserva es de 8 horas.");

            // Validar que el espacio exista y esté activo
            RuleFor(r => r.SpaceId)
                .MustAsync(async (spaceId, cancellation) => await SpaceExistsAndIsActive(spaceId))
                .WithMessage("El espacio seleccionado no es válido o no está activo.");

            // Validar que el usuario exista y esté activo
            RuleFor(r => r.UserId)
                .MustAsync(async (userId, cancellation) => await UserExistsAndIsActive(userId))
                .WithMessage("El usuario no es válido o no está activo.");

            // Validar solapamientos con reservas existentes
            RuleFor(r => r)
                .MustAsync(async (reservation, cancellation) => await IsReservationConflictFree(reservation))
                .WithMessage("El espacio ya está reservado para el rango de tiempo especificado.");

            // Validar que el usuario no tenga reservas solapadas
            RuleFor(r => r)
                .MustAsync(async (reservation, cancellation) => await IsUserAvailable(reservation))
                .WithMessage("El usuario ya tiene una reserva en el rango de tiempo especificado.");
        }

        // Validación personalizada: Verificar si el espacio existe y está activo
        private async Task<bool> SpaceExistsAndIsActive(Guid spaceId)
        {
            var space = await _spaceRepository.GetByIdAsync(spaceId);
            return space != null && space.IsActive;
        }

        // Validación personalizada: Verificar si el usuario existe y está activo
        private async Task<bool> UserExistsAndIsActive(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null && user.IsActive;
        }

        // Validación personalizada: Verificar conflictos de reservas en el mismo espacio
        private async Task<bool> IsReservationConflictFree(Reservation reservation)
        {
            var existingReservations = await _reservationRepository.GetReservationsAsync(
                startDate: reservation.StartDate,
                endDate: reservation.EndDate,
                spaceId: reservation.SpaceId,
                userId: null);

            return !existingReservations.Any(r =>
                r.StartDate < reservation.EndDate && r.EndDate > reservation.StartDate);
        }

        // Validación personalizada: Verificar que el usuario no tenga reservas solapadas
        private async Task<bool> IsUserAvailable(Reservation reservation)
        {
            var userReservations = await _reservationRepository.GetReservationsAsync(
                startDate: reservation.StartDate,
                endDate: reservation.EndDate,
                spaceId: null,
                userId: reservation.UserId);

            var validate = !userReservations.Any(r =>
                r.StartDate < reservation.EndDate && r.EndDate > reservation.StartDate);
            return validate;
        }
    }

}
