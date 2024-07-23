using Microsoft.AspNetCore.Mvc;
using Entities.DTO;
using Business.Abstract;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Core.Entities.Concrete;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Business.Constants;
using Core.Utilities.Security.JWT;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Azure;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserService _userService;
        private  IHttpContextAccessor _httpRequest;

        public AuthController(IAuthService authService, IUserService userService, IHttpContextAccessor httpRequest)
        {
            _authService = authService;
            _userService = userService;
            _httpRequest = httpRequest;
        }

        [HttpGet("getUserInfo")]
        public IActionResult GetUserInfo()
        {
            var userInfoJson = Request.Cookies["userInfo"];
            if(userInfoJson != null)
            {
                var userInfo = JsonConvert.DeserializeObject<IUserService>(userInfoJson);
                return Ok(userInfo);
            }
            
            return NotFound();
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
       
            var usertologin = _authService.Login(userForLoginDto);

            if (!usertologin.Success)
            {
                return BadRequest(usertologin.Message);
            }

            var result = _authService.CreateAccessToken(usertologin.Data);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

       
        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] UserForLoginDto userForLogin)
        {
            var usertoLogin = _authService.Login(userForLogin);

            if (usertoLogin == null)
            {
                return BadRequest(usertoLogin.Message);
            }
            if (usertoLogin.Success)
            {
                Response.Cookies.Delete("PersistentCookie");
                var claims = new List<Claim>
            {
                new Claim(type:ClaimTypes.Email, value: userForLogin.Email),
                new Claim(type:ClaimTypes.Name, value: usertoLogin.Data.FirstName)
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true,
                    });
                var cookieOptions = new CookieOptions()
                {
                    
                    HttpOnly = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None, 
                    Secure = false,
                    Expires = DateTime.UtcNow.AddSeconds(20),
                };

                return Ok(usertoLogin.Success);
            }
            return Unauthorized();
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var userClaims = User.Claims.Select(x => new Claim(x.Type, x.Value)).ToList();
            return Ok(userClaims);
        }
        [HttpGet("signout")]
        public async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if(!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
