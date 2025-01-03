namespace ReservationApp.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid SpaceId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Space? Space { get; set; } // Propiedad de navegación al espacio
        public User? User { get; set; } // Propiedad de navegación al usuario
        public bool ConflictsWith(Reservation other) =>
            SpaceId == other.SpaceId &&
            StartDate < other.EndDate &&
            EndDate > other.StartDate;
    }
}
