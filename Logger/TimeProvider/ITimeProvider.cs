using System;

namespace Util.TimeProvider
{
    public interface ITimeProvider
    {
        DateTime GetNowTime();
    }
}
