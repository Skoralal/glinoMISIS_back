using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Core.Models
{
    public class Employee : PrivateEmployee
    {
        public string HashedPassword { get; set; } = "";
    }
}
