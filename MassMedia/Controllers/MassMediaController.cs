using FluentValidation;
using MassMedia.Application.Models;
using MassMedia.Application.Services;
using MassMedia.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MassMedia.Controllers
{
    [Route("Mass-Media")]
    public class MassMediaController : Controller
    {
        private readonly IMassMediaService _massMediaService;
        private readonly IValidator<MassMediaRequest> _validator;
        private readonly IUserService _userService;

        public MassMediaController(IMassMediaService massMediaService, IValidator<MassMediaRequest> validator,
            IUserService userService)
        {
            _massMediaService = massMediaService;
            _validator = validator;
            _userService = userService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetMassMedias()
        {
            var response = await _massMediaService.GetAllAsync();

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpGet]
        [Route("{id:guid}/News")]
        public async Task<IActionResult> GetArticles(Guid id)
        {
            var response = await _massMediaService.GetArticlesAsync(id);

            return View("~/Views/Article/List.cshtml",response.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> News(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetUser();

                return user?.WorkPlaceId == id || user?.Role == Role.Admin ? Redirect($"/Mass-Media/{id}/Sended-News") : 
                    Redirect($"/Mass-Media/{id}/News");
            }

            return Redirect($"/Mass-Media/{id}/News");
        }

        [HttpGet("{id:guid}/Sended-News"), Authorize]
        public async Task<IActionResult> GetSendedArticles(Guid id)
        {
            var response = await _massMediaService.GetSendedAsync(id);

            return View("~/Views/Article/AcceptList.cshtml", response.Data);
        }

        [HttpGet("Search/{query}")]
        public async Task<IActionResult> Search(string query)
        {
            var response = await _massMediaService.SearchAsync(query);

            if (response.Success)
            {
                var entities = response!.Data!.Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                });

                return Ok(entities);
            }

            return BadRequest(response.Error);
        }

        [HttpPost(""),Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] MassMediaRequest model)
        {
            try
            {
                _validator.ValidateAndThrow(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _massMediaService.AddAsync(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpGet("Create"),Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        private async Task<UserEntity?> GetUser()
        {
            var userId = Guid.Parse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

            var user = await _userService.GetAsync(userId);

            return user;
        }
    }
}
