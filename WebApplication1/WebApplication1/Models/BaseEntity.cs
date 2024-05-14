namespace WebApplication1.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
	}
}
