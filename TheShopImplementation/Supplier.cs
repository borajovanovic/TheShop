using System.Collections.Generic;
using System.Linq;
using TheShopInterfaces;

namespace TheShopImplementation
{
    public class Supplier : ISupplier
    {
        public string SuplierName { get; set; }
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
