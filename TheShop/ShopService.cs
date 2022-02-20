using System;
using System.Collections.Generic;

namespace TheShop
{
    public class ShopService
	{
		private DatabaseDriver DatabaseDriver;
		private Logger logger;

		private Supplier Supplier1;
		private Supplier Supplier2;
		private Supplier Supplier3;
		
		public ShopService()
		{
			this.DatabaseDriver = new DatabaseDriver();
			this.logger = new Logger();
			this.Supplier1 = new Supplier() { SuplierName = "Suplier1", InventoryArticles = new List<Article>() { new Article(1, "Article from supplier1", 458) }};
			this.Supplier2 = new Supplier() { SuplierName = "Suplier2", InventoryArticles = new List<Article>() { new Article(1, "Article from supplier2", 459) } };
			this.Supplier3 = new Supplier() { SuplierName = "Suplier3", InventoryArticles = new List<Article>() { new Article(1, "Article from supplier3", 460) } };

		}

		public void OrderAndSellArticle(int articleId, int maxExpectedArticlePrice, int buyerId)
		{
			#region ordering article

			Article article = null;
			Article tempArticle = null;
			var articleExists = this.Supplier1.IsArticleInInventory(articleId);
			if (articleExists)
			{
				tempArticle = this.Supplier1.GetArticle(articleId);
				if (maxExpectedArticlePrice < tempArticle.ArticlePrice)
				{
					articleExists = this.Supplier2.IsArticleInInventory(articleId);
					if (articleExists)
					{
						tempArticle = this.Supplier2.GetArticle(articleId);
						if (maxExpectedArticlePrice < tempArticle.ArticlePrice)
						{
							articleExists = this.Supplier3.IsArticleInInventory(articleId);
							if (articleExists)
							{
								tempArticle = this.Supplier3.GetArticle(articleId);
								if (maxExpectedArticlePrice < tempArticle.ArticlePrice)
								{
									article = tempArticle;
								}
							}
						}
					}
				}
			}
			
			article = tempArticle;
			#endregion

			#region selling article

			if (article == null)
			{
				throw new Exception("Could not order article");
			}

			logger.Debug("Trying to sell article with id=" + articleId);

			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerUserId = buyerId;
			
			try
			{
				DatabaseDriver.SaveArticle(article);
				logger.Info("Article with id=" + articleId + " is sold.");
			}
			catch (ArgumentNullException ex)
			{
				logger.Error("Could not save article with id=" + articleId);
				throw new Exception("Could not save article with id");
			}
			catch (Exception)
			{
			}

			#endregion
		}

		public Article GetArticleByArticleId(int id)
		{
			return this.DatabaseDriver.GetArticleByArticleId(id);
		}
	}

}
