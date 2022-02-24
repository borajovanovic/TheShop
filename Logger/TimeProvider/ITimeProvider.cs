using System;

namespace Util.TimeProvider
{
    /// <summary>
    /// Represent TimeProvider interface
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Gets the now time.
        /// </summary>
        /// <returns></returns>
        DateTime GetNowTime();
    }
}
