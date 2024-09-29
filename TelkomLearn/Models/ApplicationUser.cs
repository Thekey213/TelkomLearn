using Microsoft.AspNetCore.Identity;

namespace TelkomLearn.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName;
        public string LastName;
    }
}
