using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class NewCNStatusEventModel : PageModel
    {
        private readonly ILogger<EncloseAndStatusModel> _logger;
        private readonly IEncloseAndCNStatusRepository _db;


        [BindProperty]
        public EncloseAndCNStatus EncloseAndCNStatus { get; set; }

        public CainiaoStatusEvent CnStatusEvent = new CainiaoStatusEvent();

        public NewCNStatusEventModel(ILogger<EncloseAndStatusModel> logger, IEncloseAndCNStatusRepository db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult OnGet(string? id, int? Order)
        {
            _logger.LogDebug($"id: {id} Order: {Order}");
            ViewData["ActivePage"] = "EncloseAndStatuses";

            IdAndOwnerId idAndOwnerId = new IdAndOwnerId(id);
            EncloseAndCNStatus = _db.GetById(idAndOwnerId.Id, idAndOwnerId.OwnerId);
            CnStatusEvent = new CainiaoStatusEvent();

            if (CnStatusEvent == null)
                return RedirectToPage("/Error");

            return Page();
        }

        /*
          public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (ConvercionType.Id > 0)
            {
                if (_db.Update(ConvercionType) == null)
                    return RedirectToPage("/ConvercionTypes/ConvercionType", new { id = ConvercionType.Id });
            }
            else
            {
                ConvercionType? convercionType = _db.Insert(ConvercionType);
                if (convercionType == null)
                    return RedirectToPage("/Error");

                ConvercionType = convercionType;
            }

            return RedirectToPage("/ConvercionTypes/ConvercionType", new { id = ConvercionType.Id });
        }
         */
    }
}
