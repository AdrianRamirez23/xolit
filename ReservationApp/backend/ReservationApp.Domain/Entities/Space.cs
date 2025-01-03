using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReservationApp.Domain.Entities
{
    public class Space
    {
        public Guid Id { get; set; } // Identificador único del espacio
        public string Name { get; set; } // Nombre del espacio (ej. "Sala de juntas A")
        public string Description { get; set; } // Descripción breve del espacio
        public int Capacity { get; set; } // Capacidad máxima del espacio
        public bool IsActive { get; set; } // Indicador de si el espacio está activo o disponible

        // Relación con las reservas
        [JsonIgnore] // Evita serializar la lista de reservas
        public ICollection<Reservation>? Reservations { get; set; }
    }

}
