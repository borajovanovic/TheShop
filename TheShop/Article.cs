using System;

namespace TheShop
{
    public class Article
	{
        public Article(int id, string articleName, int articlePrice)
        {
			this.Id = id;
			this.ArticleName = articleName;
			this.ArticlePrice = articlePrice;
        }	
		public int Id { get; set; }

		public string ArticleName { get; set; }

		public int ArticlePrice { get; set; }
		public bool IsSold { get; set; }

		public DateTime SoldDate { get; set; }
		public int BuyerUserId { get; set; }
	}

}
