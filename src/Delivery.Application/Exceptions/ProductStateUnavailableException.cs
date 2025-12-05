namespace Delivery.Application.Exceptions
{
    public class ProductStateUnavailableException : Exception
    {
        public ProductStateUnavailableException(string message) : base(message) { }
    }
}
