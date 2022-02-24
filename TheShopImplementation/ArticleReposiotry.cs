using ShopInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShopImplementation
{
    //in memory implementation
    internal class ArticleReposiotry : IArticleRepository
    {
        private readonly HashSet<Article> articles = new HashSet<Article>();

        public IEnumerable<Article> GetArticleByArticleId(int id)
        {
            return articles.Where(x => x.Id == id);
        }

        public void SaveArticle(Article article)
        {
            if (this.articles.Contains(article))
            {
                return;
            }
            this.articles.Add(article);
        }
    }

}
