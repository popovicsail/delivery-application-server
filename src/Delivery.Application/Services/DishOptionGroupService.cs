using AutoMapper;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;

public class DishOptionGroupService : IDishOptionGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DishOptionGroupService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishOptionGroupResponseDto>> GetAllAsync()
    {
        var groups = await _unitOfWork.DishOptionGroups.GetAllAsync();
        return _mapper.Map<IEnumerable<DishOptionGroupResponseDto>>(groups);
    }

    public async Task<DishOptionGroupResponseDto?> GetOneAsync(Guid id)
    {
        var group = await _unitOfWork.DishOptionGroups.GetOneAsync(id);
        if (group == null) throw new NotFoundException($"DishOptionGroup {id} not found.");
        return _mapper.Map<DishOptionGroupResponseDto>(group);
    }

    public async Task<DishOptionGroupResponseDto> AddAsync(DishOptionGroupCreateRequestDto request)
    {
        var group = _mapper.Map<DishOptionGroup>(request);
        await _unitOfWork.DishOptionGroups.AddAsync(group);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<DishOptionGroupResponseDto>(group);
    }

    public async Task<DishOptionGroupResponseDto> UpdateAsync(Guid id, DishOptionGroupUpdateRequestDto request)
    {
        var group = await _unitOfWork.DishOptionGroups.GetOneAsync(id);
        if (group == null) throw new NotFoundException($"DishOptionGroup {id} not found.");

        _mapper.Map(request, group);

        foreach (var optionUpdate in request.DishOptions)
        {
            var existing = group.DishOptions.FirstOrDefault(o => o.Id == optionUpdate.Id);
            if (existing != null)
            {
                _mapper.Map(optionUpdate, existing);
            }
            else
            {
                var newOption = _mapper.Map<DishOption>(optionUpdate);
                group.DishOptions.Add(newOption);
            }
        }

        _unitOfWork.DishOptionGroups.Update(group);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<DishOptionGroupResponseDto>(group);
    }

    public async Task DeleteAsync(Guid id)
    {
        var group = await _unitOfWork.DishOptionGroups.GetOneAsync(id);
        if (group == null) throw new NotFoundException($"DishOptionGroup {id} not found.");

        _unitOfWork.DishOptionGroups.Delete(group);
        await _unitOfWork.CompleteAsync();
    }
}
