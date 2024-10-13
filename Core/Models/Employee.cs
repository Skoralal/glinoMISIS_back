using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Core.Models
{
    public class Employee
    {
        [Key]
        public string Login { get; set; } = "";
        public string HashedPassword { get; set; } = "";
        public string Name { get; set; } = "No Name";
        public string MiddleName { get; set; } = "No Middle Name";
        public string LastName { get; set; } = "No Last Name";  
        public string Position { get; set; } = "";
        private List<PlaceOfWork> _PreviousPositions = new();
        public List<PlaceOfWork> PreviousPositions
        {
            get => _PreviousPositions;
            set
            {
                foreach (var place in value)
                {
                    place.Id = Name + LastName + place.CompanyName + place.JoinDate;
                }
                _PreviousPositions = value;
            }
        }
        public int CurrentConpartmentID { get; set; } = 0;
        public string Photo { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Hitler_portrait_crop.jpg/274px-Hitler_portrait_crop.jpg";
        public List<string> Certificates { get; set; } = new();
        public string Wallet = "";
    }
}
