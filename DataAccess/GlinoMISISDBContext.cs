using Microsoft.EntityFrameworkCore;

using Core.Models;
namespace DataAccess
{
    public class GlinoMISISDBContext : DbContext
    {
        public GlinoMISISDBContext(DbContextOptions<GlinoMISISDBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
