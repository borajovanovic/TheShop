using ShopInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Logger;
using Util.TimeProvider;

namespace ShopImplementation
{
    public class ShopService : IShopService
    {
        private readonly IArticleRepository articleRepository;
        private readonly ISupplierService supplierService;
        private readonly ILogger logger;
        private readonly ITimeProvider timeProvider;

        public ShopService(IArticleRepository articleRepository, ILogger logger, ITimeProvider timeProvider, ISupplierService supplierService)
        {
            this.articleRepository = articleRepository;
            this.logger = logger;
            this.timeProvider = timeProvider;
            this.supplierService = supplierService;

        }

        public Article OrderArticle(int articleId, int maxExpectedArticlePrice)
        {
            Article minimumPriceArticle = this.FindArticleWithMinimumPrice(articleId, maxExpectedArticlePrice);

            return minimumPriceArticle ?? throw new Exception("Could not order article");
        }

        public void SellArticle(Article article, int buyerId)
        {

            this.logger.LogMessage($"Trying to sell article with ID = {article.Id}", LogLevel.Debug);

            article.IsSold = true;
            article.SoldDate = timeProvider.GetNowTime();
            article.BuyerUserId = buyerId;

            try
            {
                this.articleRepository.SaveArticle(article);
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


        private Article FindArticleWithMinimumPrice(int articleId, int maxExpectedArticlePrice)
        {
            Article article = null;
            Article tempArticle;

            foreach (Supplier supplier in this.supplierService.GetSupliers())
            {
                tempArticle = supplier.Inventory.InventoryArticles.FirstOrDefault(x => x.Id == articleId);

                if (tempArticle != null && maxExpectedArticlePrice >= tempArticle.ArticlePrice)
                {
                    article = tempArticle;
                    maxExpectedArticlePrice = article.ArticlePrice;
                }

            }
            return article;
        }
    }

}
