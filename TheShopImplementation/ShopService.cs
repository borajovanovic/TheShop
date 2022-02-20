using LoggerImplementation;
using System;
using System.Collections.Generic;
using TheShopInterfaces;

namespace TheShopImplementation
{
    public class ShopService : IShopService
    {
        private DatabaseDriver DatabaseDriver;
        private Logger logger;

        private Supplier Supplier1;
        private Supplier Supplier2;
        private Supplier Supplier3;

        public ShopService()
        {
            DatabaseDriver = new DatabaseDriver();
            logger = new Logger();
            Supplier1 = new Supplier() { SuplierName = "Suplier1", InventoryArticles = new List<Article>() { new Article(1, "Article from supplier1", 458) } };
            Supplier2 = new Supplier() { SuplierName = "Suplier2", InventoryArticles = new List<Article>() { new Article(1, "Article from supplier2", 459) } };
            Supplier3 = new Supplier() { SuplierName = "Suplier3", InventoryArticles = new List<Article>() { new Article(1, "Article from supplier3", 460) } };

        }

        public Article OrderArticle(int articleId, int maxExpectedArticlePrice)
        {
            Article article = null;
            Article tempArticle = null;
            var articleExists = Supplier1.IsArticleInInventory(articleId);
            if (articleExists)
            {
                tempArticle = Supplier1.GetArticle(articleId);
                if (maxExpectedArticlePrice < tempArticle.ArticlePrice)
                {
                    articleExists = Supplier2.IsArticleInInventory(articleId);
                    if (articleExists)
                    {
                        tempArticle = Supplier2.GetArticle(articleId);
                        if (maxExpectedArticlePrice < tempArticle.ArticlePrice)
                        {
                            articleExists = Supplier3.IsArticleInInventory(articleId);
                            if (articleExists)
                            {
                                tempArticle = Supplier3.GetArticle(articleId);
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
            return article;

        }

        public void SellArticle(Article article, int buyerId)
        {

            if (article == null)
            {
                throw new Exception("Could not order article");
            }

            logger.Debug("Trying to sell article with ID =" + article.Id);

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;

            try
            {
                DatabaseDriver.SaveArticle(article);
                logger.Info("Article with id=" + article.Id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with ID =" + article.Id);
                throw new Exception("Could not save article with ID");
            }
            catch (Exception)
            {
            }
        }

        public Article GetArticleByArticleId(int id)
        {
            return DatabaseDriver.GetArticleByArticleId(id);
        }
    }

}
