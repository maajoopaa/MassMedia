using FluentValidation;
using MassMedia.Application.Models;
using MassMedia.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MassMedia.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserLoginRequest> _loginValidator;
        private readonly IValidator<UserRegisterRequest> _regValidator;

        public AccountController(IUserService userService, IValidator<UserLoginRequest> loginValidator, IValidator<UserRegisterRequest> regValidator)
        {
            _userService = userService;
            _loginValidator = loginValidator;
            _regValidator = regValidator;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            try
            {
                _loginValidator.ValidateAndThrow(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _userService.LoginAsync(model, HttpContext);

            return response.Success ? Ok() : BadRequest(response.Error);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest model)
        {
            try
            {
                _regValidator.ValidateAndThrow(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _userService.RegisterAsync(model, HttpContext);

            return response.Success ? Ok() : BadRequest(response.Error);
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return PartialView();
        }

        [Route("Logout"),Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }

        [HttpGet("Search/{query}"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchAsync(string query)
        {
            var response = await _userService.SearchAsync(query);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
