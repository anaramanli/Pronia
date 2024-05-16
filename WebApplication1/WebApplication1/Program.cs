using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ProniaContext>(options => options.UseSqlServer(builder.Configuration["Connect:Home"]));
builder.Services.AddDbContext<ProniaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Home")));


var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute("areas","{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
