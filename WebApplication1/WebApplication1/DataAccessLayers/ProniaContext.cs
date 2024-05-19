using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1.DataAccessLayers
{
    public class ProniaContext : IdentityDbContext
    {
        public ProniaContext(DbContextOptions<ProniaContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        //------------------------------------------------------------>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedTime = DateTime.Now;
                            entity.IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            entity.UpdatedTime = DateTime.Now;
                            break;
                    }
                }
            };
            return base.SaveChangesAsync(cancellationToken);
        }
        //------------------------------------------------------------>

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //	//options.UseSqlServer("Server=CA-R214-PC18\\SQLEXPRESS;Database=Pronia;Trusted_Connection=true;TrustServerCertificate=True;");
        //	options.UseSqlServer("Server=WINDOWS-TPN1V5P\\SQLEXPRESS;Database=Pronia;Trusted_Connection=true;TrustServerCertificate=True;");
        //	base.OnConfiguring(options);
        //}
    }
}
