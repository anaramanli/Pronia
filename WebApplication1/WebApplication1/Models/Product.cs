using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Product : BaseEntity
	{
		[MaxLength(128)]
		public string Name { get; set; }

		[Range(0, 9999999)]
		public decimal CostPrice { get; set; }

		[Range(0, 9999999)]
		public decimal SellPrice { get; set; }
		[Range(0, 100)]
		public int Discount { get; set; }
		[Range (int.MinValue,int.MaxValue)]
		public int StockCount { get; set; }
		public string ImageUrl { get; set; }
        public float Rating { get; set; }
        public ICollection<ProductImage>? Images { get; set;}
        public ICollection<ProductCategory>? ProductCategories  { get; set; }

	}
}
