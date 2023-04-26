using doan.DTO;
using doan.DTO.AppUser;
using doan.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace doan.Interface
{
    public interface IUserService
    {
        public Task<string> Authencate(AppUserLogin request);

        public Task<IdentityResult> Register(AppUserRegistration request);

        public Task<IdentityResult> ChangePassword(AppUserChangePassword request);
        public Task<bool> confirmEmail(string code, string userId);
        public Task<string> generateForgotPasswordToken(string username);
    }
}
