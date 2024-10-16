using Core.Models;

namespace Application.Services
{
    public interface IUserService
    {
        Task<List<Employee>> GetAll();
        Task<Employee?> GetByLogin(string login);
        Task<PrivateEmployee?> GetPrivateByLogin(string login);
        Task<PublicEmployee?> GetPublicByLogin(string login);
        Task<string> Login(string login, string password);
        Task Register(Employee employee, string password);
        Task <List<Compartment>> GetAllCompartments();
        Task<bool> AddCompartment(Compartment compartment);
    }
}