using System.ComponentModel.DataAnnotations;

namespace GameApi.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [MaxLength(30)]
        public string Class { get; set; } = "";

        public int Level { get; set; } = 1;

        public int Hp { get; set; } = 100;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;
    }
}