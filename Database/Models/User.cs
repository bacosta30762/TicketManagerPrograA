using Microsoft.AspNetCore.Identity;

namespace Database.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
