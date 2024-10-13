using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Skill
    {
        //public string Id { get; set; }
        public string Group { get; set; } = "no group";
        public string Description { get; set; } = "no Description";
    }
}
