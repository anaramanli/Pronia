using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Products
{
	public class CreateProductVM
	{
		public string Name { get; set; }
		[Range(0, 9999999)]
		public decimal CostPrice { get; set; }

		[Range(0, 9999999)]
		public decimal SellPrice { get; set; }
		[Range(0, 100)]
		public int Discount { get; set; }
		[Range(int.MinValue, int.MaxValue)]
		public int StockCount { get; set; }
		public IFormFile ImageFile { get; set; }
		public float Rating { get; set; }
        public int[] CategoryIds { get; set; }
        public IEnumerable<IFormFile>? ImageFiles { get; set; }
	}
}
