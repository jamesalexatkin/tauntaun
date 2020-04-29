using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tauntaun
{
    class MapInstallException : Exception
    {
        public MapInstallException()
        {
        }

        public MapInstallException(string message)
            : base(message)
        {
        }

        public MapInstallException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
