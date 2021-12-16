using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _db;
        public Customer Customer { get; set; }

        public EditModel(ICustomerRepository db)
        {
            _db = db;
        }


        public IActionResult OnGet(int Id)
        {
            Customer? customer = _db.GetCustomerById(Id);
            if (customer == null)
                return RedirectToPage("/NotFound");

            Customer = customer;
            return Page();
        }

        public IActionResult OnPost(Customer customer)
        {
            if (_db.Update(customer))
              return RedirectToPage("/Customers/Customer", new { id = customer.Id });

            return RedirectToPage("/NotFound");
        }
    }
}
