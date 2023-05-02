using doan.DTO;
using doan.DTO.AppUser;
using doan.Entities;
using doan.Helpers;
using doan.Interface;
using doan.Services;
using doan.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUser _appuser;
        private readonly IUriService _IUriService;

        public AppUserController(IAppUser appuser, IUriService iUriService)
        {
            _appuser = appuser;
            _IUriService = iUriService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUser([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            var result = await _appuser.getAllUser(filter);

            var pagedReponse = PaginationHelper.CreatePagedReponse<AppUserGet>(result.Item1, result.Item2, result.Item3, _IUriService, route);

            return new JsonResult(pagedReponse);
        }
        [HttpGet("name")]
        public async Task<ActionResult<AppUserGet>> getUserByName(string name)
        {
            var user = await _appuser.getUserByName(name);
            if (user == null)
            {
                return NotFound();

            }
            return new JsonResult(user);
        }
        [HttpGet("role")]
        public async Task<IActionResult> getRoleByName(string role)
        {
            var result = await _appuser.getUserRole(role);
            return Ok(new
            {
                status = 200,
                value = new JsonResult(result)
            });
        }
        [HttpPut("{username}")]
        public async Task<ActionResult<AppUserGet>> updateUser([FromRoute(Name = "name")] string username,AppUserChangeRequest request)
        {
            if (!ModelState.IsValid) return new JsonResult( new { success = false, message = "Item modified failed" });
            
            await _appuser.updateUser(username,request);
            var result = new { success = true, message = "Item modified successfully" };
            return new JsonResult(result);
        }


    }
}
