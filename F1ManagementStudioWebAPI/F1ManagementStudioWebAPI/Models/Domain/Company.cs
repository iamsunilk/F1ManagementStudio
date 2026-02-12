namespace F1ManagementStudioWebAPI.Models.Domain
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }

        public Guid DriverId { get; set; }
        public Guid CarId { get; set; }

        public Driver Driver { get; set; }
        public Car Car { get; set; }


    }
}
