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
            ViewData["ActivePage"] = "EncloseAndCNStatusList";
            EncloseAndCNStatuses = _db.GetAll(null);
        }
    }
}
