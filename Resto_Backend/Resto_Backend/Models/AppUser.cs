using Microsoft.AspNetCore.Identity;

namespace Resto_Backend.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
