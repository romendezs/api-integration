using DragonBall.Domain.Interfaces.ExternalServices;
using DragonBall.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DragonBallApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DragonBallController : ControllerBase
    {
        private readonly DragonBallApiService _dragonBallApiService;

        public DragonBallController(IDragonBallApiService dragonBallApiService)
        {
            _dragonBallApiService = (DragonBallApiService?)dragonBallApiService;
        }

        [HttpGet("characters")]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _dragonBallApiService.GetCharactersAsync();
            return Ok(characters);
        }

        [HttpGet("characters/{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var character = await _dragonBallApiService.GetCharacterByIdAsync(id);
            return Ok(character);
        }
    }

}
