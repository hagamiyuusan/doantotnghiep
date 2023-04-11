using System.ComponentModel.DataAnnotations;

namespace doan.DTO
{
    public class AppUserLogin
    {
        [Required(ErrorMessage = "Password is required")]

        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]

        public string? Password { get; set; }
    }
}
