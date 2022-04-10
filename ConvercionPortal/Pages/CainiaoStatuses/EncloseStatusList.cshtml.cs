using Data.Models.Cainiao;
using Data.Stores.Cainiao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseStatusListModel : PageModel
    {
        const int PageSize = 20;
        private static Dictionary<int, string> TroubleFlagValues = new Dictionary<int, string>() {
            {1, "Проблемы" },
            {2, "Без проблем" }
        };

        private static Dictionary<int, string> FinishedFlagValues = new Dictionary<int, string>() {
            {1, "Активные" },
            {2, "Завершённые" }
        };

        private readonly ILogger<EncloseStatusListModel> _logger;
        private readonly IEncloseEventsStore _db;
        public long PageCount { get; set; }

        public SelectList TroubleFlagOptions { get; set; }

        public SelectList FinishedFlagOptions { get; set; }

        public List<EncloseEvent> EncloseEvents { get; set; }
                  
        public EncloseStatusListModel(ILogger<EncloseStatusListModel> logger, IEncloseEventsStore db)
        {
            _logger = logger;
            _db = db;
            PageNumber = 0;
            TroubleFlagOptions = new SelectList(TroubleFlagValues, "Key", "Value");
            FinishedFlagOptions = new SelectList(FinishedFlagValues, "Key", "Value");
            _db.AddScopeRelation("SearchEncloseId", () => SearchEncloseId);
            _db.AddScopeRelation("SearchEncloseOwnerId", () => SearchEncloseOwnerId);
            _db.AddScopeRelation("SearchTroubleFlag", () => SearchTroubleFlag);
            _db.AddScopeRelation("SearchFinishedFlag", () => SearchFinishedFlag);
        }
        public async Task OnGetAsync()
        {
            _logger.LogDebug("OnGet");
            ViewData["ActivePage"] = "EncloseAndStatuses";

            PageCount = await _db.CountAsync() / PageSize + 1;
            EncloseEvents = await _db.GetAsync(PageNumber * PageSize, PageSize);           
        }

        [BindProperty(SupportsGet = true)]
        public string SearchEncloseId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEncloseOwnerId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchTroubleFlag { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchFinishedFlag { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }
    }
}
