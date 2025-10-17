using AutoMapper;
using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Application.Dtos.Users.WorkerDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Delivery.Application.Services;

public class WorkerService : IWorkerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public WorkerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<WorkerSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Worker> workers = await _unitOfWork.Workers.GetAllAsync();

        var response = new List<WorkerSummaryResponseDto>();

        foreach (var worker in workers)
        {
            response.Add(_mapper.Map<WorkerSummaryResponseDto>(worker));
        }
        return response;
    }

    public async Task<WorkerDetailResponseDto?> GetOneAsync(Guid id)
    {
        var worker = await _unitOfWork.Workers.GetOneAsync(id);

        if (worker == null)
        {
            throw new NotFoundException($"Worker with ID '{id}' was not found.");
        }

        var workerDto = _mapper.Map<WorkerDetailResponseDto>(worker);

        return workerDto;
    }

    public async Task<WorkerDetailResponseDto> AddAsync(WorkerCreateRequestDto request)
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
            throw new BadRequestException("ERROR: Error while creating new worker.");
        }

        await _userManager.AddToRoleAsync(user, "Worker");

        var worker = new Worker { UserId = user.Id };
        await _unitOfWork.Workers.AddAsync(worker);
        await _unitOfWork.CompleteAsync();

        var createdWorker = await _unitOfWork.Workers.GetOneAsync(worker.Id);

        return _mapper.Map<WorkerDetailResponseDto>(createdWorker);
    }

    public async Task UpdateAsync(Guid id, WorkerUpdateRequestDto workerDto)
    {
        var worker = await _unitOfWork.Workers.GetOneAsync(id);

        if (worker == null)
        {
            throw new NotFoundException($"Worker with ID '{id}' was not found.");
        }

        _mapper.Map(workerDto, worker);

        await _unitOfWork.Workers.UpdateAsync(id, worker);

        await _unitOfWork.CompleteAsync();

        return;
    }

    public async Task DeleteAsync(Guid id)
    {
        var worker = await _unitOfWork.Workers.GetOneAsync(id);

        if (worker == null)
        {
            throw new NotFoundException($"Worker with ID '{id}' was not found.");
        }

        await _unitOfWork.Workers.DeleteAsync(id, worker);

        await _unitOfWork.CompleteAsync();

        return;
    }
}
