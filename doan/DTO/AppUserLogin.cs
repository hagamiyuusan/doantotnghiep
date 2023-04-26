using System.ComponentModel.DataAnnotations;

namespace doan.DTO
{
    public class AppUserLogin
    {
        public string? UserName { set; get; }

<<<<<<< HEAD
        public string? UserName { get; set; }
=======
>>>>>>> e077d4d (add validate email)

        [Required(ErrorMessage = "Password is required")]

        public string? Password { get; set; }
    }
}
