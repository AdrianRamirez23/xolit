using Microsoft.EntityFrameworkCore;
using ReservationApp.Domain.Entities;
using System.Collections.Generic;

namespace ReservationApp.Api.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Space
            modelBuilder.Entity<Space>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Space>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Space>()
                .Property(s => s.Description)
                .HasMaxLength(250);

            // Configuración de la entidad User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            // Relación: Space -> Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Space)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SpaceId);

            // Relación: User -> Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);
        }
    }
}
