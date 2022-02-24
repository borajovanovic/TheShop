using System.Collections.Generic;

namespace ShopInterfaces
{
    /// <summary>
    /// Represent ArticleRepository Interface
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Gets the article by article identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<Article> GetArticleByArticleId(int id);

        /// <summary>
        /// Saves the article.
        /// </summary>
        /// <param name="article">The article.</param>
        void SaveArticle(Article article);
    }
}