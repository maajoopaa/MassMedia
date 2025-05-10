using FluentValidation;
using MassMedia.Application.Models;
using MassMedia.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MassMedia.Controllers
{
    [Route("Article")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IValidator<ArticleRequest> _validator;

        public ArticleController(IArticleService articleService, IValidator<ArticleRequest> validator)
        {
            _articleService = articleService;
            _validator = validator;
        }

        [HttpPost(""),Authorize]
        public async Task<IActionResult> Send([FromBody] ArticleRequest model)
        {
            try
            {
                _validator.ValidateAndThrow(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _articleService.SendAsync(model,HttpContext);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpGet("Create"),Authorize]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _articleService.GetAsync(id);

            return PartialView("~/Views/Article/Page.cshtml", response);
        }

        [HttpPut("{id:guid}"),Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] ArticleRequest model)
        {
            try
            {
                _validator.ValidateAndThrow(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _articleService.UpdateAsync(id,model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpDelete("{id:guid}"),Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _articleService.DeleteAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPut("{id:guid}/Change-Status"),Authorize]
        public async Task<IActionResult> Accept(Guid id, bool accept)
        {
            var response = await _articleService.ChangeStatusAsync(id, accept);

            return response.Success ? Ok() : BadRequest(response.Error);
        }
    }
}
