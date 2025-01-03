using ReservationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApp.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync(DateTime? startDate, DateTime? endDate, Guid? spaceId, Guid? userId);
        Task AddReservationAsync(Reservation reservation);
        Task RemoveReservationAsync(Guid id);
    }

}
