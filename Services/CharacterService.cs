using GameApi.dat;
using GameApi.DTOs;
using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Services
{
    public class CharacterService
    {
        private readonly AppDbContext _context;

        public CharacterService(AppDbContext context)
        {
            _context = context;
        }

        // Tao nhan vat
        public async Task<Character> Create(
            CreateCharacterDto dto, int accountId)
        {
            var character = new Character
            {
                Name = dto.Name,
                Class = dto.Class,
                Level = 1,
                Hp = 100,
                AccountId = accountId
            };

            _context.Characters.Add(character);

            await _context.SaveChangesAsync();

            return character;
        }

        // Cap nhat nhan vat
        public async Task<Character?> Update(
            int id, UpdateCharacterDto dto, int accountId)
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.AccountId == accountId);

            if (character == null)
            {
                return null;
            }

            character.Name = dto.Name;
            character.Class = dto.Class;
            character.Level = dto.Level;
            character.Hp = dto.Hp;

            await _context.SaveChangesAsync();

            return character;
        }
    }
}