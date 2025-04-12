using DragonBall.Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonBallAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DragonBallController(IDragonBallService service) : ControllerBase
    {
        [HttpGet("characters")]
        [Authorize]
        public async Task<IActionResult> GetAllCharacters()
        {
            var characters = await service.GetAllCharacters();
            return Ok(characters);
        }

        [HttpGet("characters/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var character = await service.GetCharacterById(id);
            if (character == null)
                return NotFound();
            return Ok(character);
        }

        [HttpGet("transformations")]
        [Authorize]
        public async Task<IActionResult> GetAllTransformations()
        {
            var transformations = await service.GetAllTransformations();
            return Ok(transformations);
        }
        
        [HttpGet("by-name")]
        [Authorize]
        public async Task<IActionResult> GetCharactersByName([FromQuery] string name)
        {
            var characters = await service.GetCharactersByNameAsync(name);
            return Ok(characters);
        }

        [HttpGet("by-affiliation")]
        [Authorize]
        public async Task<IActionResult> GetCharactersByAffiliation([FromQuery] string affiliation)
        {
            var characters = await service.GetCharactersByAffiliationAsync(affiliation);
            return Ok(characters);
        }

        // Sync me ٩(◕‿◕｡)۶
        [HttpPost("sync")]
        public async Task<IActionResult> SyncCharacters()
        {
            var result = await service.SyncCharacters();

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok("Characters successfully synchronized.");
        }
    }
}
