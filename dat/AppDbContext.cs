using Microsoft.EntityFrameworkCore;
using GameApi.Models;

namespace GameApi.dat
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Account)
                .WithMany()
                .HasForeignKey(c => c.AccountId);

            modelBuilder.Entity<Character>()
                .Property(c => c.Level)
                .HasDefaultValue(1);

            modelBuilder.Entity<Character>()
                .Property(c => c.Hp)
                .HasDefaultValue(100);
        }
    }
}