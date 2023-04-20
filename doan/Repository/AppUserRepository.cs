using doan.DTO;
using doan.DTO.AppUser;
using doan.EF;
using doan.Entities;
using doan.Interface;
using doan.Wrapper;
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

        public async Task<(List<AppUserGet>, PaginationFilter, int)> getAllUser(PaginationFilter filter)
        {
            List<AppUserGet> result = new List<AppUserGet>();

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.key);

            if (!String.IsNullOrEmpty(filter.key))
            {
                var userFilter = await _userManager.Users.Where(x => x.UserName == filter.key)
                    .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

                var countFilter = await _userManager.Users.Where(x => x.UserName == filter.key).CountAsync();


                foreach (var user in userFilter)
                {
                    var tempUse = new AppUserGet
                    {
                        Id = user.Id,
                        Username = user.UserName
                    };
                    result.Add(tempUse);
                }

                return (result, validFilter, countFilter);
            }

            var listUser = await _userManager.Users
                    .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();
            foreach (var user in listUser)
            {
                var tempUse = new AppUserGet
                {
                    Id = user.Id,
                    Username = user.UserName
                };
                result.Add(tempUse);
            }
            var count = await _userManager.Users.CountAsync();
            return (result, validFilter, count);

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

        public async Task<bool> updateUser(AppUserChangeRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.name);
            if (user == null)
            {

                return false;
            }
            user.Email = request.email;
            await _userManager.UpdateAsync(user);
            var result = await getUserbyID(request.email);
            return true;


        }


    }
}
