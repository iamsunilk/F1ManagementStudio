using Microsoft.VisualBasic;

namespace F1ManagementStudioWebAPI.Models.Domain
{
    public class Driver
    {
        public Guid DriverId { get; set; }
        public string DriverName { get; set; }
        public int Age { get; set; }

        public ICollection<Company> Companies { get; set; }
        
    }
}
