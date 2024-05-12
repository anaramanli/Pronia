using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Sliders
{
	public class UpdateSliderVM
	{
		[MaxLength(32), Required]
		public string Title { get; set; }
		[Range(0, 100)]
		public int Discount { get; set; }
		[MaxLength(64)]
		public string Subtitle { get; set; }
		public string ImageUrl { get; set; }
	}
}
