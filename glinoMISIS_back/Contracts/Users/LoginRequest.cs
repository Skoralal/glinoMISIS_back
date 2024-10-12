using Core.Models;

namespace glinoMISIS_back.Contracts.Users
{
    public class LoginRequest
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}
