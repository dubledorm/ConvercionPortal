using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseStatusListModel : PageModel
    {
        private readonly ILogger<EncloseStatusListModel> _logger;

        private readonly ICnEncloseStatusRepository _db;

        public IEnumerable<CnEncloseStatus> EncloseAndCNStatuses { get; set; }
                  
        public EncloseStatusListModel(ILogger<EncloseStatusListModel> logger, ICnEncloseStatusRepository db)
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
