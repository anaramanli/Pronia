namespace WebApplication1.Models
{
    public class Category : BaseEntity  
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
