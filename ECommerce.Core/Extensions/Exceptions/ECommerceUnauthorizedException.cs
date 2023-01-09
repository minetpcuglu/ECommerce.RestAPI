

namespace ECommerce.Core.Extensions.Exceptions
{
    public class ECommerceUnauthorizedException : System.Exception
    {
        public ECommerceUnauthorizedException()
        {
        }

        public ECommerceUnauthorizedException(string message) : base(message)
        {
        }
        public ECommerceUnauthorizedException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
