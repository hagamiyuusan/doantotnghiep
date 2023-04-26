using System.ComponentModel.DataAnnotations;

namespace doan.DTO.AppUser
{
    public class ForgotPasswordRequest
    {
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
ErrorMessage = "Email không hợp lệ")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string? email { set; get; }
    }
}
