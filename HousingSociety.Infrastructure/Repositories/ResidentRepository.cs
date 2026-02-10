
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Domain;
using HousingSociety.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HousingSociety.Infrastructure.Repositories;

public class ResidentRepository : IResidentRepository
{
    private readonly AppDbContext _db;
    public ResidentRepository(AppDbContext db) => _db = db;

    public Task<List<Resident>> GetAllAsync(CancellationToken ct = default)
        => _db.Residents.AsNoTracking().OrderBy(x => x.FullName).ToListAsync(ct);

    public Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Residents.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<List<Resident>> GetBySocietyAsync(Guid societyId, CancellationToken ct = default)
        => _db.Residents.AsNoTracking().Where(x => x.SocietyId == societyId)
           .OrderBy(x => x.FullName).ToListAsync(ct);

    public Task<Resident?> GetByEmailAsync(string email, CancellationToken ct = default)
        => _db.Residents.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, ct);

    public async Task AddAsync(Resident entity, CancellationToken ct = default)
    {
        await _db.Residents.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(Resident entity, CancellationToken ct = default)
    {
        var exists = await _db.Residents.AnyAsync(x => x.Id == entity.Id, ct);
        if (!exists) return false;
        _db.Residents.Update(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _db.Residents.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;
        _db.Residents.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
