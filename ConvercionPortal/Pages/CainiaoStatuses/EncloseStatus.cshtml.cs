using ConvercionPortal.Models;
using Data.Models.Cainiao;
using Data.Stores.Cainiao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseStatusModel : PageModel
    {
        private readonly ILogger<EncloseStatusModel> _logger;
        private readonly IEncloseEventsStore _db;
        [BindProperty]
        public EncloseEvent? encloseEvent { get; set; }

        public EncloseStatusModel(ILogger<EncloseStatusModel> logger, IEncloseEventsStore db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int EncloseId, int EncloseOwnerId)
        {
            _logger.LogDebug($"EncloseId: {EncloseId}, EncloseOwnerId: {EncloseOwnerId}");

            ViewData["ActivePage"] = "EncloseAndStatuses";
            encloseEvent = await _db.GetAsync(EncloseId, EncloseOwnerId);

            if (encloseEvent == null)
                return NotFound($"Не существует записи с EncloseId = {EncloseId} и EncloseOwnerId = {EncloseOwnerId}");

            return Page();
        }
    }
}
