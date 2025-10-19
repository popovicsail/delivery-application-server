using AutoMapper;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Interfaces;


namespace Delivery.Application.Services;

public class AllergenService : IAllergenService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AllergenService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<AllergenSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Allergen> allergens = await _unitOfWork.Allergens.GetAllAsync();

        var response = new List<AllergenSummaryResponseDto>();

        foreach (var allergen in allergens)
        {
            response.Add(_mapper.Map<AllergenSummaryResponseDto>(allergen));
        }
        return response;
    }

    public async Task<AllergenDetailResponseDto?> GetOneAsync(Guid id)
    {
        var allergen = await _unitOfWork.Allergens.GetOneAsync(id);

        if (allergen == null)
        {
            throw new NotFoundException($"Allergen with ID '{id}' was not found.");
        }

        var allergenDto = _mapper.Map<AllergenDetailResponseDto>(allergen);

        return allergenDto;
    }

    public async Task<AllergenDetailResponseDto> AddAsync(AllergenCreateRequestDto allergenDto)
    {
        var allergen = _mapper.Map<Allergen>(allergenDto);

        await _unitOfWork.Allergens.AddAsync(allergen);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<AllergenDetailResponseDto>(allergen);
    }

    public async Task UpdateAsync(Guid id, AllergenUpdateRequestDto allergenDto)
    {
        var allergen = await _unitOfWork.Allergens.GetOneAsync(id);

        if (allergen == null)
        {
            throw new NotFoundException($"Allergen with ID '{id}' was not found.");
        }

        _mapper.Map(allergenDto, allergen);

        _unitOfWork.Allergens.Update(allergen);

        await _unitOfWork.CompleteAsync();

        return;
    }

    public async Task DeleteAsync(Guid id)
    {
        var allergen = await _unitOfWork.Allergens.GetOneAsync(id);

        if (allergen == null)
        {
            throw new NotFoundException($"Allergen with ID '{id}' was not found.");
        }

        _unitOfWork.Allergens.Update(allergen);

        await _unitOfWork.CompleteAsync();

        return;
    }
}
