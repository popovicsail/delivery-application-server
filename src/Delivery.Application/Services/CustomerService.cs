using AutoMapper;
using Delivery.Application.Dtos.Users.CustomerDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Delivery.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<CustomerSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Customer> customers = await _unitOfWork.Customers.GetAllAsync();
        return _mapper.Map<List<CustomerSummaryResponseDto>>(customers.ToList());
    }

    public async Task<CustomerDetailResponseDto?> GetOneAsync(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetOneAsync(id);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID '{id}' was not found.");
        }

        return _mapper.Map<CustomerDetailResponseDto>(customer);
    }

    public async Task<CustomerDetailResponseDto> AddAsync(CustomerCreateRequestDto request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException("ERROR: Error while creating new customer.");
        }

        await _userManager.AddToRoleAsync(user, "Customer");

        var customer = new Customer { UserId = user.Id };
        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.CompleteAsync();

        var createdCustomer = await _unitOfWork.Customers.GetOneAsync(customer.Id);

        return _mapper.Map<CustomerDetailResponseDto>(createdCustomer);
    }

    public async Task UpdateAsync(Guid id, CustomerUpdateRequestDto customerDto)
    {
        var customer = await _unitOfWork.Customers.GetOneAsync(id);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID '{id}' was not found.");
        }

        _mapper.Map(customerDto, customer);

        _unitOfWork.Customers.Update(customer);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetOneAsync(id);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID '{id}' was not found.");
        }

        _unitOfWork.Customers.Update(customer);

        await _unitOfWork.CompleteAsync();
    }
}
