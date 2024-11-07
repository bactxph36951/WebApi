using Microsoft.AspNetCore.Identity;

namespace Datas.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
    }
}
