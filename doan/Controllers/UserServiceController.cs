using doan.DTO;
using doan.DTO.AppUser;
using doan.Entities;
using doan.Interface;
using doan.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public UserServiceController(IEmailSender emailSender, IUserService userService, UserManager<AppUser> userManager)
        {
            _emailSender = emailSender;
            _userService = userService;
            _userManager = userManager;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> confirmEmail(string code, string userId)
        {
            return  await _userService.confirmEmail(code, userId);
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

            var user = await _userManager.FindByNameAsync(request.UserName);
            var resultUser = await _userManager.FindByNameAsync(request.UserName);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.ActionLink(
                 action: nameof(confirmEmail),
                 values:
                     new
                     {
                         code = code,
                         userId = user.Id.ToString(),
                         
                     },
                 protocol: Request.Scheme);
                        await _emailSender.SendEmailAsync(request.Email,
                        "Xác nhận địa chỉ email",
                        @$"Bạn đã đăng ký tài khoản trên Image Captioning, 
                                               hãy <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>bấm vào đây</a> 
                                               để kích hoạt tài khoản.");
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
