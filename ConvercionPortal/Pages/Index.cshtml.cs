using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICustomerRepository _db;

        public IndexModel(ILogger<IndexModel> logger, ICustomerRepository db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
        }
    }
}