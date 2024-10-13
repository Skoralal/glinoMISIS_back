using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class PlaceOfWork
    {
        [Key]
        public string Id { get; set; }
        public string CompanyName { get; set; } = "Not specified";
        public long JoinDate { get; set; }
        public long LeaveDate { get; set; }

    }
}
