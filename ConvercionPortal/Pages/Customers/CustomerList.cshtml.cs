using ConvercionPortal.Models;
using ConvercionPortal.Services;
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
            //Dictionary<string, string> filter = new Dictionary<string, string>();
            //if (searchorders != null)
            //    filter.Add("name", searchorders);
            Customers = _db.GetAllCustomers(createFiltersDictionary(Request.Query));
        }

        private Dictionary<string, string> createFiltersDictionary(IQueryCollection Query)
        {
            Dictionary<string, string> translateAttributtes = new() { { "searcholders", "Name" }, 
                { "description", "Description" } };

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var queryKey in Query.Keys)
                if (translateAttributtes.ContainsKey(queryKey))
                  result.Add(translateAttributtes[queryKey], Query[queryKey]);
            return result;
        }
    }
}
