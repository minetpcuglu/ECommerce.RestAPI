

namespace ECommerce.Core.Extensions.Exceptions
{
    public class ECommerceNotImplementedException : System.Exception
    {
        public ECommerceNotImplementedException()
        {
        }

        public ECommerceNotImplementedException(string message) : base(message)
        {
        }
        public ECommerceNotImplementedException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
