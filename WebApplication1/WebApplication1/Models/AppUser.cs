using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
