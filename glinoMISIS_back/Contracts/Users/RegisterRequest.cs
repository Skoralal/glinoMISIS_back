using Core.Models;

namespace glinoMISIS_back.Contracts.Users
{
    public class RegisterRequest
    {
        public Employee Employee { get; set; }
        public string password { get; set; }
    }
}
