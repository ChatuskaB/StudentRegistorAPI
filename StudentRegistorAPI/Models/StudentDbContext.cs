using Microsoft.EntityFrameworkCore;

namespace StudentRegistorAPI.Models
{
    public class StudentDbContext : DbContext   
    {
        public DbSet<Student> students { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
    }
}



