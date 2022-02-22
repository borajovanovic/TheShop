using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Logger
{
    public interface ILogType
    {
        void Log(string logMessage);
        
    }
}
