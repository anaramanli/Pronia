using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ProniaContext>(options => options.UseSqlServer(builder.Configuration["Connect:Home"]));
builder.Services.AddDbContext<ProniaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Home")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ProniaContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute("areas","{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
