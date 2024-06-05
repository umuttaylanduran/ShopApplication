using Microsoft.AspNetCore.Identity;

namespace ShopApplication.UI.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }    
    }
}
