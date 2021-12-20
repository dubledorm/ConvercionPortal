using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _db;

        [BindProperty]
        public Customer Customer { get; set; }


        public EditModel(ICustomerRepository db)
        {
            _db = db;
        }


        public IActionResult OnGet(int? Id)
        {
            Customer? customer = null;
            if (Id.HasValue)
                customer = _db.GetCustomerById(Id.Value);
            else
                customer = new Customer();

            if (customer == null)
                return RedirectToPage("/Error");
            Customer = customer;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Customer.Id > 0)
            {
                if (_db.Update(Customer))
                    return RedirectToPage("/Customers/Customer", new { id = Customer.Id });
            }
            else
            {
                Customer? customer = _db.Insert(Customer);
                if (customer == null)
                    return RedirectToPage("/Error");

                Customer = customer;
            }

            return RedirectToPage("/Customers/Customer", new { id = Customer.Id });
        }
    }
}
