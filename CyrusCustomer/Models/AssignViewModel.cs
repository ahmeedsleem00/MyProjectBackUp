using CyrusCustomer.Domain.Models;
using CyrusCustomer.Domain;
using Microsoft.AspNetCore.Identity;

namespace CyrusCustomer.Models
{
    public class AssignViewModel
    {
        // Properties for the Customer
        public List<CustomerAssignmentViewModel> Customers { get; set; }
        public List<IdentityUser> Users { get; set; }
        public string SelectedUserId { get; set; }
    }
}
