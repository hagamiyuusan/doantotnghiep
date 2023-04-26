using FluentValidation;

namespace doan.DTO.AppUser
{
    public class AppUserChangePasswordValidate : AbstractValidator<AppUserChangePassword>
    {
        public AppUserChangePasswordValidate()
        {
            RuleFor(x => x.currentPassword).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.newPassword).NotEmpty().MinimumLength(6).WithMessage("Tối thiểu 6 ký tự");
            RuleFor(x => x.confirmPassword).NotEmpty().Equal(x => x.newPassword).WithMessage("Mật khẩu không trùng khớp"); 
        }
    }
}
