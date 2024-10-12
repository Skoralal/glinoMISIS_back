using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Employee
    {
        [Key]
        public string Login { get; set; } = "";
        public string HashedPassword { get; set; } = "";
    }
}
