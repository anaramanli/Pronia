using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Slider : BaseEntity
	{
		[MaxLength(32),Required]
        public string Title { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }
        [MaxLength(64)]
        public string Subtitle { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
