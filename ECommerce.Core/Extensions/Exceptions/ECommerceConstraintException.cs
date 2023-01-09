using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Extensions.Exceptions
{
    public class ECommerceConstraintException : System.Exception
    {
        public ECommerceConstraintException()
        {

        }
        public ECommerceConstraintException(string message) : base(message)
        {

        }
        public ECommerceConstraintException(string message, System.Exception inner) : base(message, inner)
        {

        }
    }
}
