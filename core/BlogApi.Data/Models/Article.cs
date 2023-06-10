using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Data.Models
{
    public class Article
    {
        public int ArticleId {get; set;}
        public string Title {get; set;}
        public string Content {get; set;}
        public DateTime CreatedDate {get; set;}
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}