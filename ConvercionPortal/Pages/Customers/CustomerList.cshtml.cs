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
            _db.AddScopeRalation("Name", ()=> SearchName);
            _db.AddScopeRalation("Description", () => SearchDescription);
        }

        public void OnGet()
        {
            ViewData["ActivePage"] = "CustomerList";
            Customers = _db.GetAllCustomers();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchDescription { get; set; }
    }
}
