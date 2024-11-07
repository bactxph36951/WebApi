using Microsoft.AspNetCore.Identity;

namespace Datas.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public string? Description { get; set; }
    }
}
