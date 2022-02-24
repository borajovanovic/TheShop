using ShopInterfaces;
using System.Linq;
using Util.Exception;
using Util.Logger;
using Util.TimeProvider;

namespace ShopImplementation
{
    internal class ShopService : IShopService
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

        public void OrderArticle(int articleId, int maxExpectedArticlePrice)
        {
            Article minimumPriceArticle = this.FindArticleWithMinimumPrice(articleId, maxExpectedArticlePrice);
            if (minimumPriceArticle == null)
            {
                throw new ArticleNotFoundException($"Could not order article with article ID = {articleId}");
            }
            minimumPriceArticle.IsOrdered = true;
            this.articleRepository.SaveArticle(minimumPriceArticle);
            this.logger.LogMessage($"Article with ID = {articleId} is successfully ordered", LogLevel.Info);
        }

        public void SellArticle(int articleId, int buyerId) // use articleId
        {
            Article article = this.GetOrderedArticleByArticleId(articleId);
            this.logger.LogMessage($"Trying to sell article with ID = {article.Id}", LogLevel.Debug);

            if (article == null)
            {
                throw new ArticleNotFoundException($"Could not sell article with article ID = {articleId}");
            }
            article.IsSold = true;
            article.SoldDate = timeProvider.GetNowTime();
            article.BuyerUserId = buyerId;

            this.articleRepository.SaveArticle(article);
            this.logger.LogMessage($"Article with ID = {article.Id} is successfully sold.", LogLevel.Info);

        }

        public Article GetSoldArticleByArticleId(int id)
        {
            // potentaly there can be more then one article with the same id. Consider that article model should be extended with one more identifier (E.g articleOrderId)
            Article article = this.articleRepository.GetArticleByArticleId(id).FirstOrDefault(x => x.IsSold);
            if (article == null)
            {
                throw new ArticleNotFoundException($"Could not find article with article ID = {id}");
            }
            return article;
        }

        public Article GetOrderedArticleByArticleId(int id)
        {
            Article article = this.articleRepository.GetArticleByArticleId(id).FirstOrDefault(x => x.IsOrdered && !x.IsSold);
            if (article == null)
            {
                throw new ArticleNotFoundException($"Could not find article with article ID = {id}");
            }
            return article;
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
