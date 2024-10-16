using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PublicEmployee
    {
        public string Name { get; set; } = "No Name";
        public string MiddleName { get; set; } = "No Middle Name";
        public string LastName { get; set; } = "No Last Name";
        public string Position { get; set; } = "";
        //private List<PlaceOfWork> _PreviousPositions = new();
        //public List<PlaceOfWork> PreviousPositions
        //{
        //    get => _PreviousPositions;
        //    set
        //    {
        //        foreach (var place in value)
        //        {
        //            place.Id = Name + LastName + place.CompanyName + place.JoinDate;
        //        }
        //        _PreviousPositions = value;
        //    }
        //}
        public List<PlaceOfWork> PreviousPositions { get; set; } = new();
        public int CurrentConpartmentID { get; set; } = 0;
        public string Photo { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Hitler_portrait_crop.jpg/274px-Hitler_portrait_crop.jpg";
        public List<string> Certificates { get; set; } = new();
        public string Wallet = "";
        public List<HardSkill> HardSkillSet { get; set; } = new();
        public List<SoftSkill> SoftSkillSet { get; set; } = new();
    }
}
