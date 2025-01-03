using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Api.Infraestructure;
using ReservationApp.Api.Infraestructure.Middleware;
using ReservationApp.Domain.Core.Validators;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var corsPolicyName = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, builder =>
    {
        builder.AllowAnyOrigin() // Reemplaza con la URL de tu frontend
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// Configuración de FluentValidation para ASP.NET Core
// Agregar servicios y dependencias
builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<Reservation>, ReservationValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
