namespace Delivery.Api.Contracts.Customers
{
    public class UpdateCustomerAllergensRequest
    {
        public List<Guid> AllergenIds { get; set; } = new();
    }
}
