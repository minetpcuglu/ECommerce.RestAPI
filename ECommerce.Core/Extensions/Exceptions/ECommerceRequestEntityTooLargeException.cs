

namespace ECommerce.Core.Extensions.Exceptions
{
    public class ECommerceRequestEntityTooLargeException : System.Exception
    {
        public ECommerceRequestEntityTooLargeException()
        {
        }

        public ECommerceRequestEntityTooLargeException(string message) : base(message)
        {
        }
        public ECommerceRequestEntityTooLargeException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
