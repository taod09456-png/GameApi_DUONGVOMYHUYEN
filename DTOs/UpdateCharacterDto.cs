using System.ComponentModel.DataAnnotations;

namespace GameApi.DTOs
{
    public class UpdateCharacterDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [MaxLength(30)]
        public string Class { get; set; } = "";

        [Range(1, 100)]
        public int Level { get; set; }

        [Range(0, 100000)]
        public int Hp { get; set; }
    }
}