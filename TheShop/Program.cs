using System;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ShopService shopService = new ShopService();

			try
			{
				//order and sell
				shopService.OrderAndSellArticle(1, 20, 10);
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