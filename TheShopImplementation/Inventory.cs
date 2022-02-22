using ShopInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShopImplementation
{
    public class Inventory
    {
        public List<Article> InventoryArticles { get; set; }

        public bool IsArticleInInventory(int id)
        {
            return InventoryArticles.Any(x => x.Id == id);
        }

        public Article GetArticle(int id)
        {
            return InventoryArticles.Single(x => x.Id == id);

        }
    }

}
