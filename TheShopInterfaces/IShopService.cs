namespace ShopInterfaces
{
    /// <summary>
    /// Represent ShopService interface
    /// </summary>
    public interface IShopService
    {
        /// <summary>
        /// Gets the sold article by article identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Article GetSoldArticleByArticleId(int id);

        /// <summary>
        /// Gets the ordered article by article identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Article GetOrderedArticleByArticleId(int id);

        /// <summary>
        /// Orders the article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="maxExpectedArticlePrice">The maximum expected article price.</param>
        void OrderArticle(int articleId, int maxExpectedArticlePrice);

        /// <summary>
        /// Sells the article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="buyerId">The buyer identifier.</param>
        void SellArticle(int articleId, int buyerId);
    }
}