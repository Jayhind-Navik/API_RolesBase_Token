using API_RolesBase_Token.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_RolesBase_Token.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
        //public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
