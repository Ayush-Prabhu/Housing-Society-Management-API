using HousingSociety.Domain;
namespace HousingSociety.Contracts
{

    public record CreateResidentDto(Guid SocietyId, string FullName, string Email, string Phone, string FlatNumber);
    public record UpdateResidentDto(string FullName, string Phone, string FlatNumber);
    public record ResidentDto(Guid Id, Guid SocietyId, string FullName, string Email, string Phone, string FlatNumber, DateTime JoinedOn);

}
