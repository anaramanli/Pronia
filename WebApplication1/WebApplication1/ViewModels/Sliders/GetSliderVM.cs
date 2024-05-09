using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Sliders
{
	public class GetSliderVM
	{
        public int Id { get; set; }
        public string Title { get; set; }
		public int Discount { get; set; }
		public string Subtitle { get; set; }
		public string ImageUrl { get; set; }
		public DateTime DateTime { get;set; }
	}
}
