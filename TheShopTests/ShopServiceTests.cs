﻿using Util.Logger;
using NSubstitute;
using NUnit.Framework;
using ShopImplementation;
using ShopInterfaces;
using System;
using Util.TimeProvider;

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

        [TestCase(458, "Article from supplier1",458)]
        [TestCase(459, "Article from supplier1", 458)]
        [TestCase(460, "Article from supplier1", 458)]

        public void ShopService_OrderArticle_LowestPriceArticleSuccessfullyFound(int maxArticlePrice, string expectedArticleName, int expectedArticlePrice)
        {
            int articleId = 1;

            Article orderedArticle = this.shopService.OrderArticle(articleId, maxArticlePrice);

            Assert.AreEqual(articleId, orderedArticle.Id);
            Assert.AreEqual(expectedArticleName, orderedArticle.ArticleName);
            Assert.AreEqual(expectedArticlePrice, orderedArticle.ArticlePrice);
        }

        [Test]
        public void ShopService_OrderArticle_ThrowException()
        {
            int articleId = 2;
            int maxArticlePrice = 20;

            Exception exception = Assert.Throws<Exception>(() => this.shopService.OrderArticle(articleId, maxArticlePrice));
            Assert.That(exception.Message, Is.EqualTo("Could not order article"));

        }


        [Test]
        public void ShopService_SellArticle_Success()
        {
            int buyerId = 10;
            int articleId = 1;
            string articleName = "MockArticle";
            int articlePrice = 100;
            Article article = new Article(articleId, articleName, articlePrice);
            DateTime nowTime = DateTime.Now;
            this.timeProvider.GetNowTime().Returns(nowTime);

            this.shopService.SellArticle(article, buyerId);

            this.logger.Received(1).LogMessage($"Trying to sell article with ID = {article.Id}", LogLevel.Debug);
            this.articleRepository.Received(1).SaveArticle(article);
            this.logger.Received(1).LogMessage($"Article with ID = {article.Id} is sold.", LogLevel.Info);
            Assert.IsTrue(article.IsSold, "Article IsSold flag should be set to true");
            Assert.AreEqual(nowTime, article.SoldDate);
            Assert.AreEqual(buyerId, article.BuyerUserId);
        }

        [Test]
        public void ShopService_GetArticleByArticleId_Sucess()
        {
            int articleId = 13;
            this.shopService.GetArticleByArticleId(articleId);
            this.articleRepository.Received(1).GetArticleByArticleId(articleId);
        }

    }
}
