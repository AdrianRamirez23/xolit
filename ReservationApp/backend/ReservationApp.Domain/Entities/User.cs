using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReservationApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } // Identificador único del usuario
        public string FullName { get; set; } // Nombre completo del usuario
        public string Email { get; set; } // Correo electrónico del usuario
        public string PhoneNumber { get; set; } // Teléfono del usuario
        public bool IsActive { get; set; } // Indicador de si el usuario está activo

        // Relación con las reservas
        [JsonIgnore] // Evita serializar la lista de reservas
        public ICollection<Reservation>? Reservations { get; set; }
    }

}
