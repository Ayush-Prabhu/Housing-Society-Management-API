
using HousingSociety.Contracts;
using HousingSociety.Domain;

namespace HousingSociety.Application.Interfaces;

public interface IServiceRequestService
{
    Task<IReadOnlyList<ServiceRequestDto>> GetAllAsync(RequestStatus? status = null, RequestType? type = null, Guid? societyId = null, Guid? residentId = null);
    Task<ServiceRequestDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ServiceRequestDto>> GetByResidentAsync(Guid residentId);
    Task<ServiceRequestDto> CreateAsync(CreateServiceRequestDto dto);
    Task<bool> UpdateStatusAsync(Guid id, UpdateServiceRequestStatusDto dto);
    Task<bool> DeleteAsync(Guid id);
}