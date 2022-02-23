using Util.Logger;
using NSubstitute;
using NUnit.Framework;
using ShopImplementation;
using ShopInterfaces;
using System;
using Util.TimeProvider;
using Util.Exception;
using System.Collections.Generic;

namespace TheShopTests
{
    [TestFixture]
    public class ShopServiceTests
    {
        private IShopService shopService;
        private IArticleRepository articleRepository;
        private ILogger logger;
        private ITimeProvider timeProvider;
        private ISupplierService supplierService;

        [SetUp]
        public void SetUp()
        {
            this.logger = Substitute.For<ILogger>();
            this.articleRepository = Substitute.For<IArticleRepository>();
            this.timeProvider = Substitute.For<ITimeProvider>();
            this.supplierService = Substitute.For<ISupplierService>();
            this.supplierService.GetSupliers().Returns(new SupplierCollectionBuilder()
              .WithSupplier(new SupplierBuilder("Suplier1").WithId(1).WithInventory(new InventoryBuilder().WithArticle(new ArticleBuilder(1, "Article from supplier1", 458))))
              .WithSupplier(new SupplierBuilder("Suplier2").WithId(2).WithInventory(new InventoryBuilder().WithArticle(new ArticleBuilder(1, "Article from supplier2", 459))))
              .WithSupplier(new SupplierBuilder("Suplier2").WithId(3).WithInventory(new InventoryBuilder().WithArticle(new ArticleBuilder(1, "Article from supplier3", 460))))
              .Build());

            this.shopService = new ShopService(this.articleRepository, this.logger, this.timeProvider, this.supplierService);
        }

        [TestCase(458, "Article from supplier1", 458)]
        [TestCase(459, "Article from supplier1", 458)]
        [TestCase(460, "Article from supplier1", 458)]

        public void ShopService_OrderArticle_LowestPriceArticleSuccessfullyFound(int maxArticlePrice, string expectedArticleName, int expectedArticlePrice)
        {
            int articleId = 1;

            this.shopService.OrderArticle(articleId, maxArticlePrice);

            this.articleRepository.Received(1).SaveArticle(Arg.Is<Article>(x => x.Id == articleId && x.ArticleName == expectedArticleName && x.ArticlePrice == expectedArticlePrice && x.IsOrdered));

        }

        [Test]
        public void ShopService_OrderArticle_ThrowArticleNotFoundException()
        {
            int articleId = 2;
            int maxArticlePrice = 20;

            Exception exception = Assert.Throws<ArticleNotFoundException>(() => this.shopService.OrderArticle(articleId, maxArticlePrice));
            Assert.That(exception.Message, Is.EqualTo($"Could not order article with article ID = {articleId}"));

        }

        [Test]
        public void ShopService_SellArticle_ThrowArticleNotFoundException()
        {
            int buyerId = 10;
            int articleId = 1;
            this.articleRepository.GetArticleByArticleId(articleId).Returns(new List<Article>());

            Exception exception = Assert.Throws<ArticleNotFoundException>(() => this.shopService.SellArticle(articleId, buyerId));
            Assert.That(exception.Message, Is.EqualTo($"Could not find article with article ID = {articleId}"));
        }


        [Test]
        public void ShopService_SellArticle_Success()
        {
            int buyerId = 10;
            int articleId = 1;
            string articleName = "MockArticle";
            int articlePrice = 100;
            Article article = new Article(articleId, articleName, articlePrice);
            article.IsOrdered = true;
            DateTime nowTime = DateTime.Now;
            this.timeProvider.GetNowTime().Returns(nowTime);
            this.articleRepository.GetArticleByArticleId(articleId).Returns(new List<Article>() { article });

            this.shopService.SellArticle(articleId, buyerId);

            this.logger.Received(1).LogMessage($"Trying to sell article with ID = {article.Id}", LogLevel.Debug);
            this.articleRepository.Received(1).SaveArticle(Arg.Is<Article>(x => x.Id == articleId && x.ArticleName == articleName && x.ArticlePrice == articlePrice && x.BuyerUserId == buyerId && x.IsOrdered && x.IsSold && x.SoldDate == nowTime));
            this.logger.Received(1).LogMessage($"Article with ID = {article.Id} is successfully sold.", LogLevel.Info);
        }

        [Test]
        public void ShopService_GetOrderedArticleByArticleId_Sucess()
        {
            int articleId = 13;
            string articleName = "MockArticle";
            int articlePrice = 100;
            Article expectedArticle = new Article(articleId, articleName, articlePrice);
            expectedArticle.IsOrdered = true;
            this.articleRepository.GetArticleByArticleId(articleId).Returns(new List<Article>() { expectedArticle });

            Article actualArticle = this.shopService.GetOrderedArticleByArticleId(articleId);
            Assert.AreEqual(expectedArticle, actualArticle);
        }

        [Test]
        public void ShopService_GetOrderedArticleByArticleId_ThrowArticleNotFoundException()
        {
            int articleId = 1;
            this.articleRepository.GetArticleByArticleId(articleId).Returns(new List<Article>());

            Exception exception = Assert.Throws<ArticleNotFoundException>(() => this.shopService.GetOrderedArticleByArticleId(articleId));
            Assert.That(exception.Message, Is.EqualTo($"Could not find article with article ID = {articleId}"));
        }

        [Test]
        public void ShopService_GetSoldArticleByArticleId_Sucess()
        {
            int articleId = 13;
            string articleName = "MockArticle";
            int articlePrice = 100;
            Article expectedArticle = new Article(articleId, articleName, articlePrice);
            expectedArticle.IsSold = true;
            this.articleRepository.GetArticleByArticleId(articleId).Returns(new List<Article>() { expectedArticle });

            Article actualArticle = this.shopService.GetSoldArticleByArticleId(articleId);
            Assert.AreEqual(expectedArticle, actualArticle);
        }

        [Test]
        public void ShopService_GetSoldArticleByArticleId_ThrowArticleNotFoundException()
        {
            int articleId = 1;
            this.articleRepository.GetArticleByArticleId(articleId).Returns(new List<Article>());

            Exception exception = Assert.Throws<ArticleNotFoundException>(() => this.shopService.GetSoldArticleByArticleId(articleId));
            Assert.That(exception.Message, Is.EqualTo($"Could not find article with article ID = {articleId}"));
        }
    }
}
