
// HousingSociety.Infrastructure/Repositories/SocietyRepository.cs
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Domain;
using HousingSociety.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HousingSociety.Infrastructure.Repositories;

public class SocietyRepository : ISocietyRepository
{
    private readonly AppDbContext _db;
    public SocietyRepository(AppDbContext db) => _db = db;

    public Task<List<Society>> GetAllAsync(CancellationToken ct = default)
        => _db.Societies.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public Task<Society?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Societies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(Society entity, CancellationToken ct = default)
    {
        await _db.Societies.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> UpdateAsync(Society entity, CancellationToken ct = default)
    {
        var exists = await _db.Societies.AnyAsync(x => x.Id == entity.Id, ct);
        if (!exists) return false;
        _db.Societies.Update(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _db.Societies.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;
        _db.Societies.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
