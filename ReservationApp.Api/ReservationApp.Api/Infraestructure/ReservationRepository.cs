using Microsoft.EntityFrameworkCore;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Api.Infraestructure
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(DateTime? startDate, DateTime? endDate, Guid? spaceId, Guid? userId)
        {
            var query = _context.Reservations.Include(a => a.Space).Include(a => a.User).AsQueryable();
           
          

            if (startDate.HasValue)
                query = query.Where(r => r.StartDate >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(r => r.EndDate <= endDate.Value);
            if (spaceId.HasValue)
                query = query.Where(r => r.SpaceId == spaceId.Value);
            if (userId.HasValue)
                query = query.Where(r => r.UserId == userId.Value);

            return await query.ToListAsync();
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveReservationAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }

}
