using Data.Models.Cainiao;
using Data.Stores.Cainiao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseStatusListModel : PageModel
    {
        const int PageSize = 3;

        private readonly ILogger<EncloseStatusListModel> _logger;
        private readonly IEncloseEventsStore _db;

        public List<EncloseEvent> EncloseEvents { get; set; }
                  
        public EncloseStatusListModel(ILogger<EncloseStatusListModel> logger, IEncloseEventsStore db)
        {
            _logger = logger;
            _db = db;
            _db.AddScopeRelation("SearchEncloseId", () => SearchEncloseId);
            _db.AddScopeRelation("SearchEncloseOwnerId", () => SearchEncloseOwnerId);
        }
        public async Task OnGetAsync()
        {
            _logger.LogDebug("OnGet");
            ViewData["ActivePage"] = "EncloseAndStatuses";
            EncloseEvents = await _db.GetAsync();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchEncloseId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEncloseOwnerId { get; set; }
    }
}
