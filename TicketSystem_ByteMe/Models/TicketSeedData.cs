using Microsoft.EntityFrameworkCore;

namespace TicketSystem_ByteMe.Models
{
  public static class TicketSeedData
  {
    public static void EnsurePopulated(IApplicationBuilder app)
    {
      TicketSystemDBContext dbContext = app
        .ApplicationServices
        .CreateScope()
        .ServiceProvider
        .GetRequiredService<TicketSystemDBContext>();

      if (dbContext.Database.GetPendingMigrations().Any())
      {
        dbContext.Database.Migrate();

      }
    }
  }

}
