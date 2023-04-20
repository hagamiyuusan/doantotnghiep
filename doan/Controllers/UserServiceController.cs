﻿using doan.DTO;
using doan.DTO.AppUser;
using doan.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserServiceController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AppUserLogin request )
        {
            var resultToken = await _userService.Authencate(request);
            if(string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or Password is incorrect");
            }
            return Ok(new { token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AppUserRegistration request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result)
            {
                return BadRequest("ERROR");
            }
            return Ok();
        }
        [HttpPost("changepassword")]
        [AllowAnonymous]
        
        public async Task<IActionResult> ChangePassword([FromBody] AppUserChangePassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ChangePassword(request);
            if (!result)
            {
                return BadRequest("ERROR");
            }
            return Ok();
        }


    }
}
