using System.ComponentModel.DataAnnotations;

namespace doan.DTO
{
    public class AppUserRegistration
    {
        [Required(ErrorMessage = "Username không được để trống")]
        [MinLength(8, ErrorMessage = "Username phải lớn hơn 8 ký tự")]
        public string? UserName { get; init; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
    ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ cái viết hoa, 1 số và 1 ký tự đặc biệt")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8,ErrorMessage = "Mật khẩu phải lớn hơn 8 ký tự")]
        public string? Password { get; init; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
    ErrorMessage = "Email không hợp lệ")]
        [Required(ErrorMessage = "Email không được để trống")]

        public string? Email { get; init; }

    }
}
