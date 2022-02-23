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
        private IEnumerable<Supplier> Suppliers;

        public ShopService(IArticleRepository articleRepository, ILogger logger, ITimeProvider timeProvider, ISupplierService supplierService)
        {
            this.articleRepository = articleRepository;
            this.logger = logger;
            this.timeProvider = timeProvider;
            this.supplierService = supplierService;

        }

        public Article OrderArticle(int articleId, int maxExpectedArticlePrice)
        {
            Article article = null;
            Article tempArticle = null;
            this.Suppliers = supplierService.GetSupliers();

            foreach (Supplier supplier in this.Suppliers)
            {
                bool articleExists = supplier.Inventory.InventoryArticles.Any(x => x.Id == articleId);
                if (articleExists)
                {
                    tempArticle = supplier.Inventory.InventoryArticles.Single(x => x.Id == articleId);
                    if (maxExpectedArticlePrice < tempArticle.ArticlePrice)
                    {
                        article = tempArticle;
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
    }

}
