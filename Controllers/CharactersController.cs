using GameApi.DTOs;
using GameApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameApi.Controllers
{
    [ApiController]
    [Route("api/characters")]
    [Authorize]
    public class CharactersController : ControllerBase
    {
        private readonly CharacterService _characterService;

        public CharactersController(
            CharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCharacterDto dto)
        {
            var accountId = int.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

            var character = await _characterService
                .Create(dto, accountId);

            return StatusCode(201, new
            {
                character.Id,
                character.Name,
                character.Class,
                character.Level,
                character.Hp,
                character.CreatedAt,
                character.AccountId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, UpdateCharacterDto dto)
        {
            var accountId = int.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

            var character = await _characterService
                .Update(id, dto, accountId);

            if (character == null)
            {
                return NotFound(new
                {
                    message = "Character not found"
                });
            }

            return Ok(new
            {
                character.Id,
                character.Name,
                character.Class,
                character.Level,
                character.Hp,
                character.CreatedAt,
                character.AccountId
            });
        }
    }
}