using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Delivery.Infrastructure.Repositories;

namespace Delivery.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IAdministratorRepository Administrators { get; private set; }
    public IAllergenRepository Allergens { get; private set; }
    public ICustomerRepository Customers { get; private set; }
    public IDishOptionGroupRepository DishOptionGroups { get; private set; }
    public IRestaurantRepository Restaurants { get; private set; }
    public IOwnerRepository Owners { get; private set; }
    public IWorkerRepository Workers { get; private set; }
    public ICourierRepository Couriers { get; private set; }
    public IDishRepository Dishes { get; private set; }
    public IOfferRepository Offers { get; private set; }
    public IOfferDishRepository OfferDishes { get; private set; }
    public IVoucherRepository Vouchers { get; private set; }
    public IFeedbackQuestionRepository FeedbackQuestions { get; private set; }
    public IFeedbackResponseRepository FeedbackResponses { get; private set; }
    public IOrderItemsRepository OrderItems { get; private set; }
    public IOrdersRepository Orders { get; private set; }
    public IAreasOfOperationRepository AreasOfOperation { get; private set; }
    public IExchangeRateRepository ExchangeRates { get; private set; }

    public IRatingRepository Rates { get; private set; }
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Administrators = new AdministratorRepository(_dbContext);
        Allergens = new AllergenRepository(_dbContext);
        Customers = new CustomerRepository(_dbContext);
        DishOptionGroups = new DishOptionGroupRepository(_dbContext);
        Restaurants = new RestaurantRepository(_dbContext);
        Owners = new OwnerRepository(_dbContext);
        Workers = new WorkerRepository(_dbContext);
        Couriers = new CourierRepository(_dbContext);
        Dishes = new DishRepository(_dbContext);
        Offers = new OfferRepository(_dbContext);
        OfferDishes = new OfferDishRepository(_dbContext);
        Vouchers = new VoucherRepository(_dbContext);
        FeedbackQuestions = new FeedbackQuestionRepository(_dbContext);
        FeedbackResponses = new FeedbackResponseRepository(_dbContext);
        OrderItems = new OrderItemsRepository(_dbContext);
        Orders = new OrdersRepository(_dbContext);
        AreasOfOperation = new AreasOfOperationRepository(_dbContext);
        Rates = new RatingRepository(_dbContext);
        ExchangeRates = new ExchangeRateRepository(_dbContext);
    }

    public Task<int> CompleteAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
