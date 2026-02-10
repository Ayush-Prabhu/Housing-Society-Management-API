
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Domain;
using HousingSociety.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HousingSociety.Infrastructure.Repositories; // << very important

public class ServiceRequestRepository : IServiceRequestRepository
{
    private readonly AppDbContext _db;
    public ServiceRequestRepository(AppDbContext db) => _db = db;

    public async Task<List<ServiceRequest>> GetAllAsync(
        int? status, int? type, Guid? societyId, Guid? residentId, CancellationToken ct = default)
    {
        var q = _db.ServiceRequests.AsNoTracking().AsQueryable();

        if (status.HasValue) q = q.Where(x => (int)x.Status == status.Value);
        if (type.HasValue) q = q.Where(x => (int)x.Type == type.Value);
        if (residentId.HasValue) q = q.Where(x => x.ResidentId == residentId.Value);

        if (societyId.HasValue)
        {
            q = from sr in _db.ServiceRequests.AsNoTracking()
                join r in _db.Residents.AsNoTracking() on sr.ResidentId equals r.Id
                where r.SocietyId == societyId.Value
                select sr;
        }

        return await q.OrderByDescending(x => x.CreatedAt).ToListAsync(ct);
    }

    public Task<ServiceRequest?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.ServiceRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<List<ServiceRequest>> GetByResidentAsync(Guid residentId, CancellationToken ct = default)
        => _db.ServiceRequests.AsNoTracking()
              .Where(x => x.ResidentId == residentId)
              .OrderByDescending(x => x.CreatedAt)
              .ToListAsync(ct);

    public async Task AddAsync(ServiceRequest entity, CancellationToken ct = default)
    {
        await _db.ServiceRequests.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(ServiceRequest entity, CancellationToken ct = default)
    {
        var exists = await _db.ServiceRequests.AnyAsync(x => x.Id == entity.Id, ct);
        if (!exists) return false;
        _db.ServiceRequests.Update(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _db.ServiceRequests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;
        _db.ServiceRequests.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
