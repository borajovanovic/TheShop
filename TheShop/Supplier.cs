using System.Collections.Generic;
using System.Linq;

namespace TheShop
{
    public class Supplier
	{
		public string SuplierName { get; set; }
		public List<Article> InventoryArticles { get; set; }
		public bool IsArticleInInventory(int id)	
		{
			return this.InventoryArticles.Any(x => x.Id == id);
		}

		public Article GetArticle(int id)
		{
			return this.InventoryArticles.Single(x => x.Id == id);
			
		}
	}

}
