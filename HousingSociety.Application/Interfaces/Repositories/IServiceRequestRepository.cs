
using HousingSociety.Domain;

namespace HousingSociety.Application.Interfaces.Repositories;

public interface IServiceRequestRepository
{
    Task<List<ServiceRequest>> GetAllAsync(
        int? status, int? type, Guid? societyId, Guid? residentId, CancellationToken ct = default);
    Task<ServiceRequest?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<ServiceRequest>> GetByResidentAsync(Guid residentId, CancellationToken ct = default);
    Task AddAsync(ServiceRequest entity, CancellationToken ct = default);
    Task<bool> UpdateAsync(ServiceRequest entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
