using Azure.Core;
using doan.Controllers;
using doan.DTO;
using doan.DTO.AppUser;
using doan.EF;
using doan.Entities;
using doan.Interface;
using doan.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

namespace doan.Repository
{
    public class UserServiceRepository : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public UserServiceRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _emailSender = emailSender;
        }

        public async Task<string> Authencate(AppUserLogin request)
        {
            
            var user = await _userManager.FindByNameAsync(request.UserName);


            if (user == null) return null ;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] 
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,String.Join(";",roles))
            };
            var token = new JwtSecurityToken(_config["JWT:ValidIssuer"], 
                _config["JWT:ValidIssuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }

        public async Task<bool> ChangePassword(AppUserChangePassword request)
        {
            var userId = await _userManager.FindByNameAsync(request.UserName);

            var result = await _userManager.ChangePasswordAsync(userId, request.Password, request.ConfirmPassword);
            
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> confirmEmail(string code, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return (result.Succeeded ? true : false);
        }

        public async Task<bool> Register(AppUserRegistration request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email

            };
            var result = await _userManager.CreateAsync(user,request.Password);

            if ( result.Succeeded )
            {

                return true;
            }
            return false;
        }
    }
}