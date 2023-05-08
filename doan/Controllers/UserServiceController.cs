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

        }

        [HttpGet("confirmregister")]
        [AllowAnonymous]
        public async Task<bool> confirmEmail(string code, string username)
        {
            return await _userService.confirmEmail(code, username);
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
                         username = user.UserName,

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
            var newtoken = HttpUtility.UrlEncode(token); // token mới
            var callbackUrl = Url.ActionLink(
                 action: nameof(ResetPassword),
                 values:
            new
            {
                username = user.UserName,
                token = newtoken

            },
                 protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(request.email,
            "Xác nhận địa chỉ email",
            @$"Bạn đã yêu cầu lấy lại mật khẩu, 
                                               <a href='{(callbackUrl)}'>bấm vào đây</a> 
                                               để lấy lại mật khẩu.");

            return Ok();

        }

        [HttpGet("resetpassword/{username}/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromRoute(Name = "username")] string username,[FromRoute(Name = "token")] string token)
        {
            //var user = await _userManager.FindByNameAsync(username);
            //var newToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            //var checkToken = await _userManager.VerifyUserTokenAsync(user, this._userManager.Options.Tokens.PasswordResetTokenProvider,
            //    "ResetPassword", newToken);
            //if (!checkToken)
            //{
            //    return BadRequest(new
            //    {
            //        status = 404,
            //        value = "Hết hạn thực hiện"
            //    }
            //    ); ;
            //}
            var hostname = $"{HttpContext.Request.Host.Host}";
            return Redirect(hostname + $":3000/sendMailChangePassword/{username}/{token}");

        }
        private async Task<bool> validateForResetPassword(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            }
            var newToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var checkToken = await _userManager.VerifyUserTokenAsync(user, this._userManager.Options.Tokens.PasswordResetTokenProvider,
                    "ResetPassword", newToken);
            if (!checkToken)
            {
                return false;
            }
            return true;
        }


        [HttpPost("resetpassword/{username}/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromRoute(Name = "username")] string username, [FromRoute(Name = "token")] string token, [FromBody] ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var validate = await this.validateForResetPassword(username, token);
                if (validate == false)
                {
                    return BadRequest(new
                    {
                        status = 404,
                        value = "Có lỗi xảy ra"
                    }
                    );
                }
                var user = await _userManager.FindByNameAsync(username);
                var newToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

                var resetResponse = await _userManager.ResetPasswordAsync(user, newToken, model.newPassword);
                if (resetResponse.Succeeded)
                {
                    return Ok(new
                    {
                        status = 200,
                        value = "Đổi mật khẩu thành công"
                    }
                ); ;
                }
                else
                {
                    return BadRequest(new
                    {
                        status = 400,
                        value = resetResponse.Errors.ToList()
                    });
                }
               
            }
            return BadRequest(new
            {
                status = 404,
                value = "Không hợp lệ"
            });
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
            return Ok(new
            {
                status = 200,
                value = "Đổi mật khẩu thành công"
            }); ;
        }


    }
}
