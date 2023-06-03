using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data.Models;

namespace BlogApi.Data.Repositories
{
    public interface IArticlesRepository
    {
        Task<List<Article>> GetAll();
        Task<Article> GetOne(int id);
        void Add(Article article);
        void Remove(Article article);
        Task SaveChanges();
    }
}