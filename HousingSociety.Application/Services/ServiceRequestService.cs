
using HousingSociety.Application.Interfaces;
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Contracts;
using HousingSociety.Domain;

namespace HousingSociety.Application.Services;

public class ServiceRequestService : IServiceRequestService
{
    private readonly IServiceRequestRepository _srRepo;
    private readonly IResidentRepository _resRepo;

    public ServiceRequestService(IServiceRequestRepository srRepo, IResidentRepository resRepo)
    {
        _srRepo = srRepo;
        _resRepo = resRepo;
    }

    public async Task<IReadOnlyList<ServiceRequestDto>> GetAllAsync(
        RequestStatus? status = null,
        RequestType? type = null,
        Guid? societyId = null,
        Guid? residentId = null)
    {
        int? st = status.HasValue ? (int)status : null;
        int? tp = type.HasValue ? (int)type : null;

        var list = await _srRepo.GetAllAsync(st, tp, societyId, residentId);
        return list.Select(Map).ToList();
    }

    public async Task<ServiceRequestDto?> GetByIdAsync(Guid id)
    {
        var sr = await _srRepo.GetByIdAsync(id);
        return sr is null ? null : Map(sr);
    }

    public async Task<IReadOnlyList<ServiceRequestDto>> GetByResidentAsync(Guid residentId)
    {
        var list = await _srRepo.GetByResidentAsync(residentId);
        return list.Select(Map).ToList();
    }

    public async Task<ServiceRequestDto> CreateAsync(CreateServiceRequestDto dto)
    {
        var resident = await _resRepo.GetByIdAsync(dto.ResidentId);
        if (resident is null)
            throw new InvalidOperationException("Resident does not exist.");

        var entity = new ServiceRequest
        {
            Id = Guid.NewGuid(),
            ResidentId = dto.ResidentId,
            Type = dto.Type,
            Title = dto.Title,
            Description = dto.Description,
            Status = RequestStatus.Open,
            CreatedAt = DateTime.UtcNow
        };

        await _srRepo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateStatusAsync(Guid id, UpdateServiceRequestStatusDto dto)
    {
        var entity = await _srRepo.GetByIdAsync(id);
        if (entity is null) return false;

        if (!IsValidTransition(entity.Status, dto.Status)) return false;

        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.UtcNow;
        if (dto.Status == RequestStatus.Closed)
            entity.ClosedAt = DateTime.UtcNow;

        return await _srRepo.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(Guid id) => _srRepo.DeleteAsync(id);

    private static bool IsValidTransition(RequestStatus from, RequestStatus to)
        => from switch
        {
            RequestStatus.Open => to is RequestStatus.InProgress or RequestStatus.Cancelled,
            RequestStatus.InProgress => to is RequestStatus.Resolved or RequestStatus.Cancelled,
            RequestStatus.Resolved => to is RequestStatus.Closed,
            _ => false // Closed/Cancelled can't transition
        };

    private static ServiceRequestDto Map(ServiceRequest s)
        => new(s.Id, s.ResidentId, s.Type, s.Title, s.Description,
               s.Status, s.CreatedAt, s.UpdatedAt, s.ClosedAt);
}