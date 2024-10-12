using Core.Models;

namespace Infrastructure
{
    public interface IJWTProvider
    {
        string GenerateToken(Employee employee);
    }
}