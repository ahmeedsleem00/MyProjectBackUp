using Microsoft.AspNetCore.Identity;

namespace CyrusCustomer.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public int CustomerId { get; set; }
    }
}