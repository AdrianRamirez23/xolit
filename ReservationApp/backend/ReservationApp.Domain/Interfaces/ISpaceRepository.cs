using ReservationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApp.Domain.Interfaces
{
    public interface ISpaceRepository
    {
        Task<Space> GetByIdAsync(Guid spaceId);
        Task<IEnumerable<Space>> GetAllAsync();
        Task AddAsync(Space space);
        Task UpdateAsync(Space space);
        Task DeleteAsync(Guid spaceId);
    }

}
