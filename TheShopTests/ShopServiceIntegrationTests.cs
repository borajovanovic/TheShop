using NUnit.Framework;
using ShopInterfaces;
using System;
using TheShop;

namespace TheShopTests
{
    [TestFixture]
    public class ShopServiceIntegrationTests
    {
        private IServiceProvider container;
        private IShopService shopService;

        [SetUp]
        public void SetUp()
        {
            this.container = ContainerBuilder.Build();
            this.shopService = this.container.GetInstance<IShopService>();

        }

        [Test]
        public void ShopService_OrderArticle_ArticleSuccessfullyFound()
        {
            int articleId = 1;
            int maxArticlePrice = 20;

            Article orderedArticle = this.shopService.OrderArticle(articleId, maxArticlePrice);

            Assert.AreEqual(articleId, orderedArticle.Id);

        }

    }
}
