using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseAndStatusModel : PageModel
    {
        private readonly ILogger<EncloseAndStatusModel> _logger;
        private readonly IEncloseAndCNStatusRepository _db;
        [BindProperty]
        public EncloseAndCNStatus? encloseAndCNStatus { get; set; }

        public EncloseAndStatusModel(ILogger<EncloseAndStatusModel> logger, IEncloseAndCNStatusRepository db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet(string id)
        {
            _logger.LogDebug($"id: {id}");
            IdAndOwnerId idAndOwnerId = new IdAndOwnerId(id);

            ViewData["ActivePage"] = "EncloseAndStatuses";
            encloseAndCNStatus = _db.GetById(idAndOwnerId.Id, idAndOwnerId.OwnerId);
        }
    }
}
