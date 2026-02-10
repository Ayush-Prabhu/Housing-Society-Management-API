
using HousingSociety.Domain;

namespace HousingSociety.Application.Interfaces.Repositories;

public interface ISocietyRepository
{
    Task<List<Society>> GetAllAsync(CancellationToken ct = default);
    Task<Society?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Society entity, CancellationToken ct = default);
    Task<bool> UpdateAsync(Society entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
