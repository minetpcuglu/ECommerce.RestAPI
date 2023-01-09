using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efor.Core.Extensions.Exceptions
{
    public class ECommerceNotFoundException : System.Exception
    {
        public ECommerceNotFoundException()
        {

        }
        public ECommerceNotFoundException(string message) : base(message)
        {

        }
        public ECommerceNotFoundException(string message, System.Exception inner) : base(message, inner)
        {

        }
    }
}
