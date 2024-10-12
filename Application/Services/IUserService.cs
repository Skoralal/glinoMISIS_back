using Core.Models;

namespace Application.Services
{
    public interface IUserService
    {
        Task<List<Employee>> GetAll();
        Task<Employee?> GetByLogin(string login);
        Task<string> Login(string login, string password);
        Task Register(Employee employee, string password);
    }
}