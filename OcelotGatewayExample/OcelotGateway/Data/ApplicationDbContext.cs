using Microsoft.EntityFrameworkCore;
using OcelotGateway.Entity;
namespace OcelotGateway.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { 
        }
        public DbSet<User> User { get; set; }
    }
}
