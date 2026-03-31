using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Entity;

namespace AuthenticationAPI.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("AppCon"));
        }

        public DbSet<User> Users { get; set; }
    }

}
