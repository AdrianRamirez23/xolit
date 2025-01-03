using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Api.Infraestructure;
using ReservationApp.Domain.Entities;
using ReservationApp.Domain.Interfaces;

namespace ReservationApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceRepository _spaceService;

        public SpacesController(ISpaceRepository spaceService)
        {
            _spaceService = spaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpaces()
        {
            var spaces = await _spaceService.GetAllAsync();
            return Ok(spaces);
        }
        [HttpPost]
        public async Task<IActionResult> AddSpace([FromBody] Space space)
        {
            if (space == null)
            {
                return BadRequest("Space data is required.");
            }

            space.Id = Guid.NewGuid(); // Generar un ID único
            await _spaceService.AddAsync(space);
            return CreatedAtAction(nameof(GetSpaces), new { id = space.Id }, space);
        }
    }
}
