namespace HousingSociety.Domain
{

    /// <summary>
    /// A service/help request raised by a resident (plumbing, electrical, etc.).
    /// </summary>
    public class ServiceRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary> Foreign key: the resident who raised this request. </summary>
        public Guid ResidentId { get; set; }

        public RequestType Type { get; set; } = RequestType.Other;

        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Open;

        //public bool CanTransitionTo(RequestStatus newStatus)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
