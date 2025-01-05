using Demo.Models;
using Microsoft.EntityFrameworkCore;
namespace Demo;

public class StudentDbContext:DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
        
    }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
            new List<Student>()
            {
                new Student() { Id = 1, Name = "Benjamin", Email = "benjamin@gmail.com" },
                new Student() { Id = 2, Name = "Benida", Email = "benida@gmail.com" },

            }
        );
        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .HasColumnType("text"); 
        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .HasColumnType("text"); 
        base.OnModelCreating(modelBuilder);
    }
}