using System.ComponentModel.DataAnnotations;

namespace Selu383.SP26.Api
{
    public class CreateLocationDto
    {
        [MaxLength(120)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public int TableCount { get; set; }
    }
}
