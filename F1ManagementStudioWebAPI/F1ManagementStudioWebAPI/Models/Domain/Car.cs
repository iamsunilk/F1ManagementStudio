namespace F1ManagementStudioWebAPI.Models.Domain
{
    public class Car
    {
        public Guid CarId { get; set; }
        public string? CarName { get; set; }
        public string? CarModel { get; set; }
        public string? year { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
