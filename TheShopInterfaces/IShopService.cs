namespace ShopInterfaces
{
    public interface IShopService
    {
        Article GetSoldArticleByArticleId(int id);
        Article GetOrderedArticleByArticleId(int id);
        void OrderArticle(int articleId, int maxExpectedArticlePrice);
        void SellArticle(int articleId, int buyerId);
    }
}