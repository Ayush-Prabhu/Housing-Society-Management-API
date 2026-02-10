namespace HousingSociety.Domain
{

    /// <summary>
    /// A resident living in a society (one society per resident).
    /// </summary>
    public class Resident
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary> Foreign key: which society the resident belongs to. </summary>
        public Guid SocietyId { get; set; }

        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string FlatNumber { get; set; } = default!;

        public bool IsActive { get; set; }  = true;
        public DateTime JoinedOn { get; set; } = DateTime.UtcNow;
    }
    }
