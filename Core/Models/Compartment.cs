using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Compartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = "No name";
    }
}
