using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Products
{
	public class GetProductVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[Range(0, 9999999)]

		public decimal SellPrice { get; set; }
		[Range(0, 100)]
		public int Discount { get; set; }
        public bool IsStock { get; set; }
        [Range(int.MinValue, int.MaxValue)]
		public string ImageUrl { get; set; }
		public float Rating { get; set; }

	}
}
