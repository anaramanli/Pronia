using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1.DataAccessLayers
{
    public class ProniaContext : DbContext
    {
        public ProniaContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=WINDOWS-TPN1V5P\\SQLEXPRESS;Database=Pronia;Trusted_Connection=true;TrustServerCertificate=True;");
            base.OnConfiguring(options);
        }
    }
}
