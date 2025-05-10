using MassMedia.Application.Services;
using MassMedia.DataAccess.Entities;
using MassMedia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace MassMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMassMediaService _massMediaService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IMassMediaService massMediaService,
            IUserService userService)
        {
            _logger = logger;
            _massMediaService = massMediaService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _massMediaService.GetAllAsync();

            return View(response.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
