using System;

namespace Util.TimeProvider
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetNowTime()
        {
            return DateTime.Now;
        }
    }
}
