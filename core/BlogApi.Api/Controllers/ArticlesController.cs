using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data.Models;
using BlogApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private IArticlesRepository _repository;

        public ArticlesController(IArticlesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<List<Article>> GetAllArticles()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetArticleById(int id)
        {
            var article = await _repository.GetOne(id);
            if(article == null) return NotFound();

            return Ok(article);
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddArticle([FromBody]Article article)
        {
            article.CreatedDate = DateTime.Now;

            _repository.Add(article);
            await _repository.SaveChanges();
            
            return CreatedAtAction("AddArticle", article);
        }

        [HttpDelete("{id}")]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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