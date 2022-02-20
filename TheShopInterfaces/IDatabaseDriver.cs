namespace TheShopInterfaces
{
    public interface IDatabaseDriver
    {
        Article GetArticleByArticleId(int id);
        void SaveArticle(Article article);
    }
}