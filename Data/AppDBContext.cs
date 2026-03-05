using EmployeePortalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalApp.Data


{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { 
        
        }
        
            public DbSet<Employee> Employees { get; set; }
    }
    
    }

       

