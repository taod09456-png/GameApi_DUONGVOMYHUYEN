using GameApi.dat;
using GameApi.DTOs;
using GameApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GameApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Account> _hasher = new();

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        // Dang ky tai khoan
        public async Task<Account?> Register(RegisterDto dto)
        {
            var exists = await _context.Accounts
                .AnyAsync(a => a.Username == dto.Username);

            if (exists)
            {
                return null;
            }

            var account = new Account
            {
                Username = dto.Username
            };

            account.PasswordHash = _hasher.HashPassword(
                account, dto.Password);

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync();

            return account;
        }

        // Dang nhap tai khoan
        public async Task<Account?> Login(LoginDto dto)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Username == dto.Username);

            if (account == null)
            {
                return null;
            }

            var result = _hasher.VerifyHashedPassword(
                account, account.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return account;
        }
    }
}