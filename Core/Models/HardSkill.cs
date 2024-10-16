using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HardSkill : Skill
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Group { get; set; } = "Hard";

    }
}
