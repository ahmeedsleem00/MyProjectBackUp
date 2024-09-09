using CyrusCustomer.DAL;
using CyrusCustomer.Domain;
using CyrusCustomer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyrusCustomer.Controllers
{
    [Route("Admin/[action]")]

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> Assign(int customerId)
        //{
        //    var customer = await _context.Customers.FindAsync(customerId);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _userManager.Users.ToListAsync();
        //    var model = new AssignViewModel
        //    {
        //        CustomerId = customerId,
        //        Customer = customer,
        //        Users = users,
        //        SelectedUserIds = _context.CustomerUserAssignments
        //   .Where(cua => cua.CustomerId == customerId)
        //   .Select(cua => cua.UserId)
        //   .ToList()
        //    };

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> AssignCustomers(string SelectedUserId, List<int> SelectedCustomerIds)
        {
            foreach (var customerId in SelectedCustomerIds)
            {
                var assignment = new CustomerUserAssignment
                {
                    CustomerId = customerId,
                    UserId = SelectedUserId
                };
                _context.CustomerUserAssignments.Add(assignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //[HttpPost]
        //public async Task<IActionResult> Assign(AssignViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var customer = await _context.Customers.FindAsync(model.CustomerId);
        //        var users = await _userManager.Users.ToListAsync();

        //        model.Customer = customer;
        //        model.Users = users;

        //        return View(model);
        //    }

        //    // Process assignments here

        //    return RedirectToAction("Index"); 
        //}


        [HttpPost]
        public async Task<IActionResult> UpdateCustomerAssignments(Dictionary<int, List<string>> userAssignments)
        {
            foreach (var assignment in userAssignments)
            {
                int customerId = assignment.Key;
                List<string> userIds = assignment.Value;

                // Remove old assignments
                var oldAssignments = _context.CustomerUserAssignments.Where(cua => cua.CustomerId == customerId);
                _context.CustomerUserAssignments.RemoveRange(oldAssignments);

                // Add new assignments
                foreach (var userId in userIds)
                {
                    _context.CustomerUserAssignments.Add(new CustomerUserAssignment
                    {
                        CustomerId = customerId,
                        UserId = userId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
