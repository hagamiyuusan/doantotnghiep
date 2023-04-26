namespace doan.DTO.AppUser
{
    public class AppUserChangePassword
    {
        public string UserName { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { set; get; }
        public string confirmPassword { set; get; }
    }
}
