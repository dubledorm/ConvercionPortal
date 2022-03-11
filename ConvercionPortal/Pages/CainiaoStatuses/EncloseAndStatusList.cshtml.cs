using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseAndStatusListModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IEncloseAndCNStatusRepository _db;

        public IEnumerable<EncloseAndCNStatus> EncloseAndCNStatuses { get; set; }
                  
        public EncloseAndStatusListModel(ILogger<IndexModel> logger, IEncloseAndCNStatusRepository db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            ViewData["ActivePage"] = "EncloseAndStatuses";
            ViewData["search-id-text"] = Request.Query.FirstOrDefault(p => p.Key == "search-id").Value;
            ViewData["search-owner-id-text"] = Request.Query.FirstOrDefault(p => p.Key == "search-owner-id").Value;
            EncloseAndCNStatuses = _db.GetAll(createFiltersDictionary(Request.Query));
        }


        private Dictionary<string, string> createFiltersDictionary(IQueryCollection Query)
        {
            Dictionary<string, string> translateAttributtes = new()
            {
                { "search-id", "Id" },
                { "search-owner-id", "OwnerId" }
            };

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var queryKey in Query.Keys)
                if (translateAttributtes.ContainsKey(queryKey))
                    result.Add(translateAttributtes[queryKey], Query[queryKey]);
            return result;
        }
    }
}
