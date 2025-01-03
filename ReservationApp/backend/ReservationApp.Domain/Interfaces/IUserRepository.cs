using ReservationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
    }

}
