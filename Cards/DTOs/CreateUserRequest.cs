using Cards.Models;
using System.ComponentModel.DataAnnotations;

namespace Cards.DTOs
{
    public class CreateUserRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
