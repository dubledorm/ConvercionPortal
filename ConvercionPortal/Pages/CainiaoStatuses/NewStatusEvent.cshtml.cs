using Data.Models.Cainiao;
using Data.Stores.Cainiao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class NewStatusEventModel : PageModel
    {
        private readonly ILogger<NewStatusEventModel> _logger;
        private readonly IEncloseEventsStore _db;
        public static string[] CainiaoStatusNames = { 
            "CAINIAO_GLOBAL_LASTMILE_GTMSACCEPT_CALLBACK", 
            "CAINIAO_GLOBAL_LASTMILE_GTMS_SC_ARRIVE_CALLBACK", 
            "Bob" 
        };

        public SelectList ListCainiaoStatuses { get; set; }

        public EncloseEvent encloseEvent { get; set; }

        [BindProperty]
        public EncloseStatusHistoryRecord historyRecord { get; set; }

        [BindProperty]
        public int encloseId { get; set; }
        [BindProperty]
        public int encloseOwnerId { get; set; }
  
        [BindProperty]
        public int? statusHistoryOrder { get; set; }

        public NewStatusEventModel(ILogger<NewStatusEventModel> logger, IEncloseEventsStore db)
        {
            _logger = logger;
            _db = db;
            ListCainiaoStatuses = new SelectList(CainiaoStatusNames.ToDictionary(k => k, v => v), "Key", "Value");  
        }

        public async Task<IActionResult> OnGetAsync(int EncloseId, int EncloseOwnerId, int? statusHistoryOrder)
        {
            _logger.LogDebug($"EncloseId: {EncloseId}, EncloseOwnerId: {EncloseOwnerId} statusHistoryOrder: {statusHistoryOrder}");
            ViewData["ActivePage"] = "EncloseAndStatuses";
            ViewData["Title"] = "Добавить событие";

            encloseEvent = await _db.GetAsync(EncloseId, EncloseOwnerId);
            if (encloseEvent == null)
                return NotFound($"Не существует записи с EncloseId = {EncloseId} и EncloseOwnerId = {EncloseOwnerId}");

            if (statusHistoryOrder.HasValue)
            {
                ViewData["Title"] = "Изменить событие";
                historyRecord = encloseEvent.StatusHistory[statusHistoryOrder.Value];
                this.statusHistoryOrder = statusHistoryOrder.Value;
            }
            else
                historyRecord = new EncloseStatusHistoryRecord();

            return Page();
        }

          public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            encloseEvent = await _db.GetAsync(encloseId, encloseOwnerId);
            if (encloseEvent == null)
                return NotFound($"Не существует записи с EncloseId = {encloseId} и EncloseOwnerId = {encloseOwnerId}");

            if (statusHistoryOrder.HasValue)
            {
                encloseEvent.StatusHistory[statusHistoryOrder.Value] = historyRecord;

            } else
            {
                encloseEvent.StatusHistory.Add(historyRecord);
            }

                
            if (_db.UpdateAsync(encloseEvent) == null)
                return RedirectToPage("/CainiaoStatuses/EncloseStatus", new { Encloseid = encloseId, EncloseOwnerId = encloseOwnerId });

            return RedirectToPage("/CainiaoStatuses/EncloseStatus", new { Encloseid = encloseId, EncloseOwnerId = encloseOwnerId });

       /*     if (ConvercionType.Id > 0)
            {
                if (_db.Update(ConvercionType) == null)
                    return RedirectToPage("/CainiaoStatuses/EncloseStatus", new { Encloseid = encloseId, EncloseOwnerId = encloseOwnerId });
            }
            else
            {
                ConvercionType? convercionType = _db.Insert(ConvercionType);
                if (convercionType == null)
                    return RedirectToPage("/Error");

                ConvercionType = convercionType;
            }

            return RedirectToPage("/ConvercionTypes/ConvercionType", new { id = ConvercionType.Id });
       */
        }

    }
}
