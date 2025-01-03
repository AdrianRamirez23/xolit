using Microsoft.EntityFrameworkCore;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Api.Infraestructure
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly AppDbContext _dbContext;

        public SpaceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Space> GetByIdAsync(Guid spaceId)
        {
            return await _dbContext.Spaces.FirstOrDefaultAsync(s => s.Id == spaceId);
        }

        public async Task<IEnumerable<Space>> GetAllAsync()
        {
            return await _dbContext.Spaces.ToListAsync();
        }

        public async Task AddAsync(Space space)
        {
            await _dbContext.Spaces.AddAsync(space);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Space space)
        {
            _dbContext.Spaces.Update(space);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid spaceId)
        {
            var space = await GetByIdAsync(spaceId);
            if (space != null)
            {
                _dbContext.Spaces.Remove(space);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
