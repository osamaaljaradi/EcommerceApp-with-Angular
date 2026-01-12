using AutoMapper;
using ECOM.API.Helper;
using ECOM.CORE.DTO;
using ECOM.CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOM.API.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpPost("Register")]
        public async Task<IActionResult> register(RegisterDTO registerDTO)
        {
            string result = await work.Auth.registerAcync(registerDTO);
            if (result != "done")
            {
                return BadRequest(new ResponseAPI(400, result));
            }
            return Ok(new ResponseAPI(200,result));
        }
        [HttpPost("Login")]
        public async Task<IActionResult>  login(LoginDTO loginDTO)
        {
            string result = await work.Auth.LoginAsync(loginDTO);
            if (result.StartsWith("please"))
            {
                return  BadRequest(new ResponseAPI(400,result));
            }

            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.Strict
            });
            return Ok(new ResponseAPI(200));
        }

        [HttpPost("active-account")]
        public async Task<IActionResult>  active(ActiveAccountDTO accountDTO)
        {
            var result = await work.Auth.activeAccount(accountDTO);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));
        }

        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> forget(string email)
        {
            var result = await work.Auth.sendEmailForForgetPassword(email);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> reset(ResetPasswordDTO resetPasswordDTO)
        {
            var result = await work.Auth.resetPassword(resetPasswordDTO);
            if(result == "done")
            {
                return Ok(new ResponseAPI(200));
            }
            return BadRequest(new ResponseAPI(400));
        }

    }
}
