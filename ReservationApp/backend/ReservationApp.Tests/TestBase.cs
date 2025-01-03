using Microsoft.EntityFrameworkCore;
using ReservationApp.Api.Infraestructure;
using System;

namespace ReservationApp.Tests
{
    public abstract class TestBase
    {
        protected AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            return new AppDbContext(options);
        }
    }
}