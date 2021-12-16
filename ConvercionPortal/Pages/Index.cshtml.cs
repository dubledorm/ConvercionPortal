using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICustomerRepository _db;

        public IEnumerable<Customer> Customers { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICustomerRepository db)
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