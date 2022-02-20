using System.Collections.Generic;
using System.Linq;
using TheShopInterfaces;

namespace TheShopImplementation
{
    //in memory implementation
    public class DatabaseDriver : IDatabaseDriver
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
