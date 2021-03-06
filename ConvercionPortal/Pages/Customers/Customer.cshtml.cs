using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.Customers
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerRepository _db;
        public Customer? Customer { get; set; }

        public CustomerModel(ICustomerRepository db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            ViewData["ActivePage"] = "CustomerList";
            Customer = _db.GetCustomerById(id);
        }
    }
}
