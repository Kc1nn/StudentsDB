using Microsoft.EntityFrameworkCore;
using StudentsDB.Models.GroupModels;
using StudentsDB.Models.StudentModels;

namespace StudentsDB.Models
{
    public class StudentsContext : DbContext
    {
        public StudentsContext(DbContextOptions<StudentsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
    }
}
