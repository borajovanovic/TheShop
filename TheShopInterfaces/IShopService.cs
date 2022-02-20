namespace TheShopInterfaces
{
    public interface IShopService
    {
        Article GetArticleByArticleId(int id);
        Article OrderArticle(int articleId, int maxExpectedArticlePrice);
        void SellArticle(Article article, int buyerId);
    }
}