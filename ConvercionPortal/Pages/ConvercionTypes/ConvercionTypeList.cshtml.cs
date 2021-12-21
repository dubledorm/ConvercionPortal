using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.ConvercionTypes
{
    public class ConvercionTypeListModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConvercionTypeRepository _db;

        public IEnumerable<ConvercionType> ConvercionTypes { get; set; }

        public ConvercionTypeListModel(ILogger<IndexModel> logger, IConvercionTypeRepository db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            ViewData["ActivePage"] = "ConvercionTypes";
            ConvercionTypes = _db.GetAll(createFiltersDictionary(Request.Query));
        }

        private Dictionary<string, string> createFiltersDictionary(IQueryCollection Query)
        {
            Dictionary<string, string> translateAttributtes = new()
            {
                { "search-name", "Name" },
                { "search-description", "Description" },
                { "search-url", "ServiceUrl" }
            };

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var queryKey in Query.Keys)
                if (translateAttributtes.ContainsKey(queryKey))
                    result.Add(translateAttributtes[queryKey], Query[queryKey]);
            return result;
        }
    }
}
