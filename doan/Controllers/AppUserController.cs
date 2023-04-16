using doan.DTO;
using doan.DTO.AppUser;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUser _appuser;

        public AppUserController(IAppUser appuser)
        {
            _appuser = appuser;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllUser()
        {
            var result = await _appuser.getAllUser();
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(result);
        }
        [HttpGet("name")]
        public async Task<ActionResult<AppUserGet>> getUserByName(string name)
        {
            var user = await _appuser.getUserbyID(name);
            if (user == null)
            {
                return NotFound();

            }
            return new JsonResult(user);
        }
        [HttpGet("role")]
        public async Task<IList<string>> getRoleByName(string role)
        {
            var result = await _appuser.getUserRole(role);
            return result;
        }
        [HttpPut("{name}")]
        public async Task<ActionResult<AppUserGet>> updateUser(string name, [FromBody]AppUserChangeRequest request)
        {
            if (!ModelState.IsValid)

                return new JsonResult( new { success = false, message = "Item modified failed" });
            await _appuser.updateUser(name, request);
            var result = new { success = true, message = "Item modified successfully" };
            return new JsonResult(result);
        }


    }
}
