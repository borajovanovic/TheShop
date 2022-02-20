using Microsoft.Extensions.DependencyInjection;
using ShopInterfaces;
using System;

namespace TheShop
{
    internal class Program
	{
		public static readonly IServiceProvider Container = ContainerBuilder.Build();

		private static void Main(string[] args)
		{
			IShopService shopService = Container.GetInstance<IShopService>();
			int articleId = 1;
			int maxArticlePrice = 20;
			int buyerId = 10;

			try
			{
				//order and sell
				Article orderedArticle = shopService.OrderArticle(articleId, maxArticlePrice);
				shopService.SellArticle(orderedArticle, buyerId);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			try
			{
				//print article on console
				Article article = shopService.GetArticleByArticleId(1);
				Console.WriteLine("Found article with ID: " + article.Id);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Article not found: " + ex);
			}

			try
			{
				//print article on console				
				Article article = shopService.GetArticleByArticleId(12);
				Console.WriteLine("Found article with ID: " + article.Id);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Article not found: " + ex);
			}

			Console.ReadKey();
		}
	}
}