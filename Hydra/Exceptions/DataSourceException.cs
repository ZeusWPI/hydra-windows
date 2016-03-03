using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Exceptions {
    public class DataSourceException : Exception {

        public DataSourceException(string message, Exception innerException) : base(message, innerException) {

        }
    }
}
