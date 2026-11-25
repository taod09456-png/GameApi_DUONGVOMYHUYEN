using System.ComponentModel.DataAnnotations;

namespace GameApi.DTOs
{
    public class CreateCharacterDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [MaxLength(30)]
        public string Class { get; set; } = "";
    }
}