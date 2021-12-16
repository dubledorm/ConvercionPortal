using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
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
            Customer = _db.GetCustomerById(id);
        }
    }
}
