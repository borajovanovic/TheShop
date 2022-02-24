using System.Collections.Generic;

namespace ShopInterfaces
{

    public interface IArticleRepository
    {
        IEnumerable<Article> GetArticleByArticleId(int id);
        void SaveArticle(Article article);
    }
}