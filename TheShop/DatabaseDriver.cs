using System.Collections.Generic;
using System.Linq;

namespace TheShop
{
    //in memory implementation
    public class DatabaseDriver
	{
		private readonly List<Article> articles = new List<Article>();

		public Article GetArticleByArticleId(int id)
		{
            return this.articles.Single(x => x.Id == id);
		}

		public void SaveArticle(Article article)
		{
			this.articles.Add(article);
		}
	}

}
