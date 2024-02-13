using System.ComponentModel.DataAnnotations;

namespace Cards.DTOs
{
    public class CreateCardRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
