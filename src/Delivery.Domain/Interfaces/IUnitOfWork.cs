namespace Delivery.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAdministratorRepository Administrators { get; }
    IAllergenRepository Allergens { get; }
    ICustomerRepository Customers { get; }
    IDishOptionGroupRepository DishOptionGroups { get; }
    IRestaurantRepository Restaurants { get; }
    IOwnerRepository Owners { get; }
    ICourierRepository Couriers { get; }
    IWorkerRepository Workers { get; }
    IDishRepository Dishes { get; }
    IVoucherRepository Vouchers { get; }
    IOrdersRepository Orders { get;}
    IOrderItemsRepository OrderItems { get; }
    Task<int> CompleteAsync();
}