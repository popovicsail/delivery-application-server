using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.CommonEntities;
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

        var response = new List<CustomerSummaryResponseDto>();

        foreach (var customer in customers)
        {
            response.Add(_mapper.Map<CustomerSummaryResponseDto>(customer));
        }
        return response;
    }

    public async Task<CustomerDetailResponseDto?> GetOneAsync(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetOneAsync(id);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID '{id}' was not found.");
        }

        var customerDto = _mapper.Map<CustomerDetailResponseDto>(customer);

        return customerDto;
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

        return;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetOneAsync(id);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID '{id}' was not found.");
        }

        _unitOfWork.Customers.Delete(customer);

        await _unitOfWork.CompleteAsync();

        return;
    }

    public async Task BirthdayVoucherBackgroundJobAsync()
    {
        var birthdayCustomers = await _unitOfWork.Customers.GetBirthdayCustomersAsync();

        if (birthdayCustomers == null || !birthdayCustomers.Any())
        {
            return;
        }

        foreach (var customer in birthdayCustomers)
        {
            var alreadyReceived = await _unitOfWork.Customers
                .HasReceivedBirthdayVoucherInLastYearAsync(customer.Id);

            if (alreadyReceived)
            {
                continue;
            }

            Voucher newVoucher = new Voucher
            {
                Name = "Birthday Voucher",
                DiscountAmount = 2000,
                CustomerId = customer.Id
            };

            await _unitOfWork.Vouchers.AddAsync(newVoucher);
        }

        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<AddressDto>> GetMyAddressesAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) throw new UnauthorizedException("Korisnik mora biti ulogovan");

        var customer = await _unitOfWork.Customers.GetOneAsync(user.Id);
        if (customer == null) throw new NotFoundException("Customer profil nije pronađen.");

        return customer.Addresses
            .Select(a => _mapper.Map<AddressDto>(a))
            .ToList();
    }

    public async Task CreateAddressAsync(ClaimsPrincipal principal, AddressCreateRequest request)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) throw new UnauthorizedException("Korisnik mora biti ulogovan");

        var customer = await _unitOfWork.Customers.GetOneAsync(user.Id);
        if (customer == null) throw new NotFoundException("Customer profil nije pronađen.");

        var newAddress = _mapper.Map<Address>(request);
        customer.Addresses.Add(newAddress);

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAddressAsync(ClaimsPrincipal principal, Guid addressId, AddressUpdateRequest request)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) throw new UnauthorizedException("Korisnik mora biti ulogovan");

        var customer = await _unitOfWork.Customers.GetOneAsync(user.Id);
        if (customer == null) throw new NotFoundException("Customer profil nije pronađen.");

        var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);
        if (address == null) throw new NotFoundException("Adresa nije pronađena.");

        _mapper.Map(request, address);

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAddressAsync(ClaimsPrincipal principal, Guid addressId)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) throw new UnauthorizedException("Korisnik mora biti ulogovan");

        var customer = await _unitOfWork.Customers.GetOneAsync(user.Id);
        if (customer == null) throw new NotFoundException("Customer profil nije pronađen.");

        var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);
        if (address == null) throw new NotFoundException("Adresa nije pronađena.");

        customer.Addresses.Remove(address);

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.CompleteAsync();
    }


    public async Task<List<Guid>> GetMyAllergensAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) throw new UnauthorizedException("Korisnik mora biti ulogovan");

        var customer = await _unitOfWork.Customers.GetOneAsync(user.Id);
        if (customer == null) throw new NotFoundException("Customer profil nije pronađen.");

        var allergenIds = customer.Allergens.Select(a => a.Id).ToList();

        return ( allergenIds );
    }

    public async Task UpdateMyAllergensAsync(ClaimsPrincipal principal, UpdateCustomerAllergensRequest request)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
            throw new UnauthorizedException("Korisnik mora biti ulogovan");

        var customer = await _unitOfWork.Customers.GetOneAsync(user.Id);
        if (customer == null)
            throw new NotFoundException("Customer profil nije pronađen.");

        // Očisti postojeće veze
        customer.Allergens.Clear();

        // Ukloni duplikate iz requesta
        var distinctIds = request.AllergenIds.Distinct().ToList();

        // Dohvati alergene po ID‑jevima
        var allergens = await _unitOfWork.Allergens.FindAsync(distinctIds);

        // Dodaj nove veze
        foreach (var allergen in allergens)
        {
            customer.Allergens.Add(allergen);
        }

        _unitOfWork.Customers.Update(customer);

        await _unitOfWork.CompleteAsync();
    }

}
