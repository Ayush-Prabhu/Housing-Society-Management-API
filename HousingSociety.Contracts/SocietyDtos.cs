using HousingSociety.Domain;
namespace HousingSociety.Contracts;

public record CreateSocietyDto(string Name, string Address, string City, string? Pincode);
public record UpdateSocietyDto(string Name, string Address, string City, string? Pincode);
public record SocietyDto(Guid Id, string Name, string Address, string City, string? Pincode, DateTime CreatedAt);
