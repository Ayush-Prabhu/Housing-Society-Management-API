
using HousingSociety.Domain;
namespace HousingSociety.Contracts
{


    public record CreateServiceRequestDto(Guid ResidentId, RequestType Type, string Title, string? Description);
    public record UpdateServiceRequestStatusDto(RequestStatus Status);
    public record ServiceRequestDto(
        Guid Id,
        Guid ResidentId,
        RequestType Type,
        string Title,
        string? Description,
        RequestStatus Status,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        DateTime? ClosedAt);


}
