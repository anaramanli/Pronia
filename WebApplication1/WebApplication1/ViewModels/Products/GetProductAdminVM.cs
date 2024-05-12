using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Products
{
	public class GetProductAdminVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
		[Range(0, 9999999)]
		public decimal CostPrice { get; set; }

		[Range(0, 9999999)]
		public decimal SellPrice { get; set; }
		[Range(0, 100)]
		public int Discount { get; set; }
		[Range(int.MinValue, int.MaxValue)]
		public int StockCount { get; set; }
		public string ImageUrl { get; set; }
		public float Rating { get; set; }


	}
}
