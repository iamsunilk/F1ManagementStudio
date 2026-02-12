namespace F1ManagementStudioWebAPI.Models.DTOs.CarDtos
{
    public class CarDto
    {
        public Guid CarId { get; set; }
        public string? CarName { get; set; }
        public string? CarModel { get; set; }
        public string? year { get; set; }
    }
}
