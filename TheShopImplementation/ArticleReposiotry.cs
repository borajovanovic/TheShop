using ShopInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShopImplementation
{
    //in memory implementation
    public class ArticleReposiotry : IArticleRepository
    {
        private readonly List<Article> articles = new List<Article>();

        public Article GetArticleByArticleId(int id)
        {
            return articles.Single(x => x.Id == id);
        }

        public void SaveArticle(Article article)
        {
            articles.Add(article);
        }
    }

}
