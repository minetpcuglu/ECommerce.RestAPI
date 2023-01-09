using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Extensions.Exceptions
{
    public class ECommerceBadRequestException : System.Exception
    {
        public ECommerceBadRequestException()
        {

        }
        public ECommerceBadRequestException(string message) : base(message)
        {

        }
        public ECommerceBadRequestException(string message, System.Exception inner) : base(message, inner)
        {

        }
    }
}
