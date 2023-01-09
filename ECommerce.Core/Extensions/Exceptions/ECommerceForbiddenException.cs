using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Extensions.Exceptions
{

    public class ECommerceForbiddenException : System.Exception
    {
        public ECommerceForbiddenException()
        {
        }

        public ECommerceForbiddenException(string message) : base(message)
        {
        }

        public ECommerceForbiddenException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
