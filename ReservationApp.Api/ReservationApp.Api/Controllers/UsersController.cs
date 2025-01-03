using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            // Validación básica
            if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("FullName and Email are required.");
            }

            // Generar un ID único
            user.Id = Guid.NewGuid();

            // Guardar el usuario
            await _userRepository.AddAsync(user);

            // Retornar respuesta exitosa
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
