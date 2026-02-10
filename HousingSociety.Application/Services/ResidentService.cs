
using HousingSociety.Application.Interfaces;
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Contracts;
using HousingSociety.Domain;

namespace HousingSociety.Application.Services;

public class ResidentService : IResidentService
{
    private readonly IResidentRepository _resRepo;
    private readonly ISocietyRepository _socRepo;

    public ResidentService(IResidentRepository resRepo, ISocietyRepository socRepo)
    {
        _resRepo = resRepo;
        _socRepo = socRepo;
    }

    public async Task<IReadOnlyList<ResidentDto>> GetAllAsync()
    {
        var residents = await _resRepo.GetAllAsync();
        return residents.Select(Map).ToList();
    }

    public async Task<ResidentDto?> GetByIdAsync(Guid id)
    {
        var resident = await _resRepo.GetByIdAsync(id);
        return resident is null ? null : Map(resident);
    }

    public async Task<IReadOnlyList<ResidentDto>> GetBySocietyAsync(Guid societyId)
    {
        var residents = await _resRepo.GetBySocietyAsync(societyId);
        return residents.Select(Map).ToList();
    }

    public async Task<ResidentDto> CreateAsync(CreateResidentDto dto)
    {
        var society = await _socRepo.GetByIdAsync(dto.SocietyId);
        if (society is null)
            throw new InvalidOperationException("Society does not exist.");

        var existingEmail = await _resRepo.GetByEmailAsync(dto.Email);
        if (existingEmail is not null)
            throw new InvalidOperationException("Email already in use.");

        var entity = new Resident
        {
            Id = Guid.NewGuid(),
            SocietyId = dto.SocietyId,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            FlatNumber = dto.FlatNumber,
            JoinedOn = DateTime.UtcNow
        };

        await _resRepo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateResidentDto dto)
    {
        var entity = await _resRepo.GetByIdAsync(id);
        if (entity is null) return false;

        entity.FullName = dto.FullName;
        entity.Phone = dto.Phone;
        entity.FlatNumber = dto.FlatNumber;

        return await _resRepo.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(Guid id) => _resRepo.DeleteAsync(id);

    private static ResidentDto Map(Resident r)
        => new(r.Id, r.SocietyId, r.FullName, r.Email, r.Phone, r.FlatNumber, r.JoinedOn);
}
