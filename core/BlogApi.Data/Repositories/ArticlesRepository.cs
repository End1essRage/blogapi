using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private BlogDbContext _context;

        public ArticlesRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(Article article)
        {
            _context.Article.Add(article);
        }

        public Task<List<Article>> GetAll()
        {
            return _context.Article.ToListAsync();
        }

        public Task<Article> GetOne(int id)
        {
            return _context.Article.SingleOrDefaultAsync(a => a.ArticleId == id);
        }

        public void Remove(Article article)
        {
            _context.Article.Remove(article);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}