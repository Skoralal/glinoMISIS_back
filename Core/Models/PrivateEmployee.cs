using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PrivateEmployee : PublicEmployee
    {
        [Key]
        public string Login { get; set; } = "";
        public List<Notification> Notifications { get; set; } = new();

    }
}
