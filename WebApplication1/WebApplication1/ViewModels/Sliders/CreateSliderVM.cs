using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Sliders
{
	public class CreateSliderVM
	{
		[MaxLength(32,ErrorMessage= "Max Length Is 32"), Required]
		public string Title { get; set; }
		[Range(0, 100,ErrorMessage ="Out of range")]
        public int Discount { get; set; }
		[MaxLength(64)]
		public string Subtitle { get; set; }
		public string ImageUrl { get; set; }
		public DateTime DateTime { get; set; }
	}
}
