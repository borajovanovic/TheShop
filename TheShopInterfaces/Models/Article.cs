using System;

namespace ShopInterfaces
{
    public class Article
    {
        public Article(int id, string articleName, int articlePrice)
        {
            Id = id;
            ArticleName = articleName;
            ArticlePrice = articlePrice;
        }
        public int Id { get; set; }

        public string ArticleName { get; set; }

        public int ArticlePrice { get; set; }
        public bool IsSold { get; set; }
        public bool IsOrdered { get; set; }

        public DateTime SoldDate { get; set; }
        public int BuyerUserId { get; set; }
    }

}
