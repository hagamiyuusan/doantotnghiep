using doan.DTO;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
        public UserServiceRepository
            (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;

        }

        public async Task<string> Authencate(AppUserLogin request)
        {
            
            var user = await _userManager.FindByNameAsync(request.Email);


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



        public async Task<bool> Register(AppUserRegistration request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email

            };
            var result = await _userManager.CreateAsync(user,request.Password);
            if( result.Succeeded )
            {
                return true;
            }
            return false;
        }
    }
}