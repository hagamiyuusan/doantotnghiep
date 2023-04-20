using System.ComponentModel.DataAnnotations;

namespace doan.DTO
{
    public class AppUserRegistration
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public string? Email { get; init; }

    }
}
