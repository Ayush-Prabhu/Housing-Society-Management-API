
using HousingSociety.Domain;

namespace HousingSociety.Application.Interfaces.Repositories;

public interface IResidentRepository
{
    Task<List<Resident>> GetAllAsync(CancellationToken ct = default);
    Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Resident>> GetBySocietyAsync(Guid societyId, CancellationToken ct = default);
    Task<Resident?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task AddAsync(Resident entity, CancellationToken ct = default);
    Task<bool> UpdateAsync(Resident entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
