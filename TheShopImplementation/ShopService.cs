using Util.Logger;
using ShopInterfaces;
using System;
using System.Collections.Generic;
using Util.TimeProvider;

namespace ShopImplementation
{
    public class ShopService : IShopService
    {
        private readonly IArticleRepository articleRepository;
        private readonly ILogger logger;
        private readonly ITimeProvider timeProvider;


        private Supplier Supplier1;
        private Supplier Supplier2;
        private Supplier Supplier3;

        public ShopService(IArticleRepository databaseDriver, ILogger logger, ITimeProvider timeProvider)
        {
            this.articleRepository = databaseDriver;
            this.logger = logger;
            this.timeProvider = timeProvider;
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
                throw new Exception("Could not order article"); //this should be in order method 
            }

            this.logger.LogMessage($"Trying to sell article with ID = {article.Id}", LogLevel.Debug);

            article.IsSold = true;
            article.SoldDate = timeProvider.GetNowTime();
            article.BuyerUserId = buyerId;

            try
            {
                articleRepository.SaveArticle(article);
                this.logger.LogMessage($"Article with ID = {article.Id} is sold.", LogLevel.Info);
            }
            catch (ArgumentNullException ex)
            {
                this.logger.LogMessage($"Could not save article with ID = {article.Id}", LogLevel.Error);  //unreachable code, it already checked that article is not null, probably should be part of databaseDriver
                throw new Exception("Could not save article with ID"); // throw in catch 
            }
            catch (Exception)
            {//remove this exception
            }
        }

        public Article GetArticleByArticleId(int id)
        {
            return this.articleRepository.GetArticleByArticleId(id);
        }
    }

}
