using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApi.Api.Services;
using BlogApi.Data.Models;
using BlogApi.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private IArticlesRepository _repository;
        private IUserService _userService;

        public ArticlesController(IArticlesRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Article>> GetAllArticles()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetArticleById(int id)
        {
            var article = await _repository.GetOne(id);
            if(article == null) return NotFound();

            return Ok(article);
        }

        [HttpPost]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddArticle([FromBody] ArticleDto articleDto)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.FindByUserNameAsync(userName);

            var article = new Article
            {
                Title = articleDto.Title,
                Content = articleDto.Content,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                Author = user
            };

            _repository.Add(article);
            await _repository.SaveChanges();
            
            return CreatedAtAction("AddArticle", article);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            var article = await _repository.GetOne(id);
            if(article == null) return NotFound();

            _repository.Remove(article);
            await _repository.SaveChanges();

            return NoContent();
        }
    }
}