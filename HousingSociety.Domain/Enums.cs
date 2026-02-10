namespace HousingSociety.Domain
{

    public enum RequestType
    {
        Plumbing = 1,
        Electrical = 2,
        Security = 3,
        Other = 4
    }

    public enum RequestStatus
    {
        Open = 1,
        InProgress = 2,
        Resolved = 3,
        Closed = 4,
        Cancelled = 5
    }

}
