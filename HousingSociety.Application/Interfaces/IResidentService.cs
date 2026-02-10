
using HousingSociety.Contracts;

namespace HousingSociety.Application.Interfaces;

public interface IResidentService
{
    Task<IReadOnlyList<ResidentDto>> GetAllAsync();
    Task<ResidentDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ResidentDto>> GetBySocietyAsync(Guid societyId);
    Task<ResidentDto> CreateAsync(CreateResidentDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateResidentDto dto);
    Task<bool> DeleteAsync(Guid id);
}
