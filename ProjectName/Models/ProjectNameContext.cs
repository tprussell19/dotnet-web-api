using Microsoft.EntityFrameworkCore;

namespace ProjectName.Models
{
  public class ProjectNameContext : DbContext
  {
    public ProjectNameContext(DbContextOptions<ProjectNameContext> options)
    : base(options)
    {
    }
    public DbSet<MainObject> MainObjects { get; set; }
  }
}