using System.Collections.Generic;

namespace ShopInterfaces
{
    public interface IArticleFilterCriteria
    {
        IEnumerable<Article> MeetCriteria(IEnumerable<Article> articles);
    }
}
