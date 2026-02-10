
using HousingSociety.Contracts;

namespace HousingSociety.Application.Interfaces;

public interface ISocietyService
{
    Task<IReadOnlyList<SocietyDto>> GetAllAsync();
    Task<SocietyDto?> GetByIdAsync(Guid id);
    Task<SocietyDto> CreateAsync(CreateSocietyDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateSocietyDto dto);
    Task<bool> DeleteAsync(Guid id);
}
