using doan.DTO;
using doan.DTO.AppUser;
using doan.Entities;
using doan.Interface;
using doan.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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
        public async Task<IActionResult> Authenticate([FromBody] AppUserLogin request)
        {
            try
            {
                var resultToken = await _userService.Authencate(request);
                if (string.IsNullOrEmpty(resultToken))
                {
                    return BadRequest("Username or Password is incorrect");
                }
                return Ok(new { token = resultToken });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> confirmEmail(string code, string userId)
        {
            return await _userService.confirmEmail(code, userId);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AppUserRegistration request)
        {

            if (!ModelState.IsValid)
            {


                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }

            var checkUser = await _userManager.FindByNameAsync(request.UserName);
            if (checkUser != null)
            {
                return BadRequest("Username đã tồn tại");
            }
            var result = await _userService.Register(request);
            if (!result.Succeeded)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }
            var user = await _userManager.FindByNameAsync(request.UserName);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = Url.ActionLink(
                 action: nameof(confirmEmail),
                 values:
                     new
                     {
                         code = token,
                         userId = user.Id.ToString(),

                     },
                 protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(request.Email,
            "Xác nhận địa chỉ email",
            @$"Bạn đã đăng ký tài khoản trên Image Captioning, 
                                               hãy <a href='{(callbackUrl)}'>bấm vào đây</a> 
                                               để kích hoạt tài khoản.");
            return Ok();
        }

        [HttpPost("password/reset")]
        [AllowAnonymous]
        public async Task<IActionResult> forgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null)
            {
                return BadRequest("Không tồn tại username");
            }
            var token = await _userService.generateForgotPasswordToken(user.UserName);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = Url.ActionLink(
                 action: nameof(ResetPassword),
                 values:
            new
            {
                email = request.email,
                token = token

            },
                 protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(request.email,
            "Xác nhận địa chỉ email",
            @$"Bạn đã yêu cầu lấy lại mật khẩu, 
                                               <a href='{(callbackUrl)}'>bấm vào đây</a> 
                                               để lấy lại mật khẩu.");

            return Ok();

        }

        [HttpGet("resetpassword/{email}/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromRoute] string email,[FromRoute] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Email không hợp lệ");
            }
            var newToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var checkToken = await _userManager.VerifyUserTokenAsync(user, this._userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", newToken);
            if (!checkToken)
            {
                return BadRequest("Hết hạn thực hiện");
            }
            return Ok(new
            {
                user.UserName,
                token
            });

        }



        [HttpPost("resetpassword/{email}/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromRoute] string email, [FromRoute] string token, [FromBody] ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var newToken = HttpUtility.HtmlDecode(token);

                var resetResponse = await _userManager.ResetPasswordAsync(user, newToken, model.newPassword);
                if (resetResponse.Succeeded)
                {
                    return Ok("Đổi mật khẩu thành công");
                }
            }
            return BadRequest("Không hợp lệ");
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
            if (!result.Succeeded)
            {
                var errors = result.Errors.SelectMany(e => e.Code).ToList();
                return BadRequest(new { errors });
            }
            return Ok();
        }


    }
}
