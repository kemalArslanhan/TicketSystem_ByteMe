using Microsoft.EntityFrameworkCore;

namespace TicketSystem_ByteMe.Models
{
  public class TicketSystemDBContext : DbContext
  {
    public TicketSystemDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
  }
}
