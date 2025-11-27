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
    IOfferRepository Offers { get; }
    IOfferDishRepository OfferDishes { get; }
    IVoucherRepository Vouchers { get; }
    IFeedbackQuestionRepository FeedbackQuestions { get; }
    IFeedbackResponseRepository FeedbackResponses { get; }
    IOrdersRepository Orders { get;}
    IOrderItemsRepository OrderItems { get; }
    IRatingRepository Rates { get; }
    Task<int> CompleteAsync();
}