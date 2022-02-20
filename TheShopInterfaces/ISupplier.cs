using System.Collections.Generic;

namespace TheShopInterfaces
{
    public interface ISupplier
    {
        List<Article> InventoryArticles { get; set; }
        string SuplierName { get; set; }

        Article GetArticle(int id);
        bool IsArticleInInventory(int id);
    }
}