using Microsoft.EntityFrameworkCore;
using TicketSystem_ByteMe.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TicketSystemDBContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddScoped<ITicketSystemRepository, EFTicketSystemRepository>();

var app = builder.Build();
TicketSeedData.EnsurePopulated(app);

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.MapControllerRoute(
   name: "admin",
   pattern: "{controller=Admin}/{action=Home}/{id?}",
defaults: new { controller = "Admin", action = "Home" });

app.Run();
