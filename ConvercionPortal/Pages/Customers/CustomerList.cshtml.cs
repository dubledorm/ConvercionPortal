using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.Customers
{
    public class CustomerListModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICustomerRepository _db;

        public IEnumerable<Customer> Customers { get; set; }

        public CustomerListModel(ILogger<IndexModel> logger, ICustomerRepository db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            Customers = _db.GetAllCustomers();
        }
    }
}
