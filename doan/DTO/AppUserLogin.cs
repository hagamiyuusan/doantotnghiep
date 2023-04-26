using System.ComponentModel.DataAnnotations;

namespace doan.DTO
{
    public class AppUserLogin
    {
        [Required(ErrorMessage = "Password is required")]
        public string? UserName { set; get; }

        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
