namespace ShopInterfaces
{
    public interface IArticleRepository
    {
        Article GetArticleByArticleId(int id);
        void SaveArticle(Article article);
    }
}