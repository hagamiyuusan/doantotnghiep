using doan.DTO;
using doan.DTO.AppUser;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace doan.Repository
{
    public class AppUserRepository : IAppUser
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;



        public AppUserRepository(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration config, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
        }

        public async Task<List<AppUserGet>> getAllUser()
        {
            List<AppUserGet> result = new List<AppUserGet>();
            var listUser = await _userManager.Users.ToListAsync();
            foreach(var user in listUser)
            {
                var tempUse = new AppUserGet
                {
                    Id = user.Id,
                    Username = user.UserName
                };
                result.Add(tempUse);
            }
  
            return result;

        }

        public async Task<AppUserGet> getUserbyID(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            if (user == null)
            {
                return null;
            }
            var gotUser = new AppUserGet()
            {
                Username = user.UserName,
                Id = user.Id
            };

            return gotUser;
        
        }

        public async Task<IList<string>> getUserRole(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            var result = await _userManager.GetRolesAsync(user);
            return result;
            
        }

        public async Task<bool> updateUser(string id, AppUserChangeRequest request)
        {
            var user = await _userManager.FindByEmailAsync(id);
            if (user == null)
            {

                return false;
            }
            user.UserName = request.name;
            user.Email = request.email;
            await _userManager.UpdateAsync(user);
            var result = await getUserbyID(request.email);
            return true ;


        }


    }
}
