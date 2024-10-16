using Core.Models;
using DataAccess;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly GlinoMISISDBContext _context;
        private readonly IJWTProvider _jwtProvider;
        public UserService(IPasswordHasher passwordHasher, GlinoMISISDBContext dbContext, IJWTProvider jWTProvider)
        {
            _passwordHasher = passwordHasher;
            _context = dbContext;
            _jwtProvider = jWTProvider;
        }
        public async Task Register(Employee employee, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            employee.HashedPassword = hashedPassword;
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }
        public async Task<Employee?> GetByLogin(string login)
        {
            var aboba = await _context.Employees.FindAsync(login);
            return aboba;
        }
        public async Task<PrivateEmployee?> GetPrivateByLogin(string login)
        {
            Employee aboba = await _context.Employees.FindAsync(login);
            var shmee = aboba as PrivateEmployee;
            return shmee;
        }
        public async Task<PublicEmployee?> GetPublicByLogin(string login)
        {
            Employee aboba = await _context.Employees.FindAsync(login);
            var shmee = aboba as PublicEmployee;
            return shmee;
        }
        public async Task<string> Login(string login, string password)
        {
            var employee = await GetByLogin(login);
            if (employee == null)
            {
                return "Wrong login or password";
            }
            var aboba = _passwordHasher.Verify(password, employee.HashedPassword);
            if (!aboba)
            {
                return "Wrong login or password";
            }
            var token = _jwtProvider.GenerateToken(employee);
            return token;
        }

        public async Task<List<Compartment>> GetAllCompartments()
        {
            var aboba = await _context.Compartments.AsNoTracking().ToListAsync();
            return aboba;
        }

        public async Task<bool> AddCompartment(Compartment compartment)
        {
            await _context.Compartments.AddAsync(compartment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PublicEmployee>> GetFellasFromCompartment(int compartmentID)
        {
            List<PublicEmployee> list = await _context.Employees.AsNoTracking().Where(x=>x.CurrentConpartmentID == compartmentID).Select(x=>x as PublicEmployee).ToListAsync();
            return list;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                var register = await GetByLogin(employee.Login);
                register.Wallet = employee.Wallet;
                register.SoftSkillSet = employee.SoftSkillSet;
                register.HardSkillSet = employee.HardSkillSet;
                register.PreviousPositions = employee.PreviousPositions;
                register.Certificates = employee.Certificates;
                register.Notifications = employee.Notifications;
                register.CurrentConpartmentID = employee.CurrentConpartmentID;
                register.Name = employee.Name;
                register.MiddleName = employee.MiddleName;
                register.LastName = employee.LastName;
                register.Photo = employee.Photo;
                await _context.SaveChangesAsync();
                return true;
            }
            catch {  return false; }
        }
    }
}
