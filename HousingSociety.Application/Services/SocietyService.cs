
using HousingSociety.Application.Interfaces;
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Contracts;
using HousingSociety.Domain;

namespace HousingSociety.Application.Services;

public class SocietyService : ISocietyService
{
    private readonly ISocietyRepository _repo;

    public SocietyService(ISocietyRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<SocietyDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync(); // no ct
        return list.Select(Map).ToList();
    }

    public async Task<SocietyDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<SocietyDto> CreateAsync(CreateSocietyDto dto)
    {
        var entity = new Society
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            Pincode = dto.Pincode,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateSocietyDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = dto.Name;
        entity.Address = dto.Address;
        entity.City = dto.City;
        entity.Pincode = dto.Pincode;

        return await _repo.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(Guid id) => _repo.DeleteAsync(id);

    private static SocietyDto Map(Society s)
        => new(s.Id, s.Name, s.Address, s.City, s.Pincode, s.CreatedAt);
}
