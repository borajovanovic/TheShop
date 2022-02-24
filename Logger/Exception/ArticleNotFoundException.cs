using System;

namespace Util.Exception
{
    public class ArticleNotFoundException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleNotFoundException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public ArticleNotFoundException(string errorMessage)
            :base(errorMessage)
        {

        }
    }
}
