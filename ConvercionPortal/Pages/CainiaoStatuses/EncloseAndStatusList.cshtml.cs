using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseAndStatusListModel : PageModel
    {
        private readonly ILogger<EncloseAndStatusListModel> _logger;

        private readonly IEncloseAndCNStatusRepository _db;

        public IEnumerable<EncloseAndCNStatus> EncloseAndCNStatuses { get; set; }
                  
        public EncloseAndStatusListModel(ILogger<EncloseAndStatusListModel> logger, IEncloseAndCNStatusRepository db)
        {
            _logger = logger;
            _db = db;
            _db.AddScopeRalation("SearchEncloseId", () => SearchEncloseId);
            _db.AddScopeRalation("SearchEncloseOwnerId", () => SearchEncloseOwnerId);
        }
        public void OnGet()
        {
            _logger.LogDebug("OnGet");
            ViewData["ActivePage"] = "EncloseAndStatuses";
            EncloseAndCNStatuses = _db.GetAll();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchEncloseId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEncloseOwnerId { get; set; }
    }
}
