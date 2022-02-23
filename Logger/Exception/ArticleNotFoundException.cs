using System;

namespace Util.Exception
{
    public class ArticleNotFoundException : SystemException
    {
        public ArticleNotFoundException(string errorMessage)
            :base(errorMessage)
        {

        }
    }
}
