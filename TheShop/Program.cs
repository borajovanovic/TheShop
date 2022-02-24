using ShopInterfaces;
using System;
using Util.Exception;

namespace TheShop
{
    internal class Program
    {
        public static readonly IServiceProvider Container = ContainerBuilder.Build();

        private static void Main(string[] args)
        {
            IShopService shopService = Container.GetInstance<IShopService>();
            int articleId = 1;
            int maxArticlePrice = 460;
            int buyerId = 10;

            //order and sell
            shopService.OrderArticle(articleId, maxArticlePrice);
            shopService.SellArticle(articleId, buyerId);

            try
            {
                //print article on console
                Article article = shopService.GetSoldArticleByArticleId(articleId);
                Console.WriteLine("Found article with ID: " + article.Id);
            }
            catch (ArticleNotFoundException exception)
            {
                Console.WriteLine($"{exception.Message}"); //Decided not to print exception since this is looked as client code
            }

            try
            {
                //print article on console				
                Article article = shopService.GetSoldArticleByArticleId(12);
                Console.WriteLine("Found article with ID: " + article.Id);
            }
            catch (ArticleNotFoundException exception)
            {
                Console.WriteLine($"{exception.Message}");
            }

            Console.ReadKey();
        }
    }
}