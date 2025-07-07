using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        

        public DbSet<TaskItem> Tasks { get; set; }
    }
}
