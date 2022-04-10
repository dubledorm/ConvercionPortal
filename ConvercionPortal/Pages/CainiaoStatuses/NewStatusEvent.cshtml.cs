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

        [BindProperty]
        public int newStatusHistoryOrder { get; set; }

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

            encloseEvent = await _db.GetByIdAsync(EncloseId, EncloseOwnerId);
            if (encloseEvent == null)
                return NotFound($"Не существует записи с EncloseId = {EncloseId} и EncloseOwnerId = {EncloseOwnerId}");

            if (statusHistoryOrder.HasValue)
            {
                ViewData["Title"] = "Изменить событие";
                historyRecord = encloseEvent.StatusHistory[statusHistoryOrder.Value];
                this.newStatusHistoryOrder = statusHistoryOrder.Value + 1;
            }
            else
            {
                this.newStatusHistoryOrder = encloseEvent.StatusHistory.Count + 1;
                historyRecord = new EncloseStatusHistoryRecord();
            }    

            return Page();
        }

          public async Task<IActionResult> OnPostAsync()
        {
            
            encloseEvent = await _db.GetByIdAsync(encloseId, encloseOwnerId);
            if (encloseEvent == null)
                return NotFound($"Не существует записи с EncloseId = {encloseId} и EncloseOwnerId = {encloseOwnerId}");

            if (!ModelState.IsValid)
                return Page();

            if (newStatusHistoryOrder < 1)
            {
                ModelState.AddModelError("newStatusHistoryOrder", "Значение должно быть больше 0");
                return Page();
            }

            if (statusHistoryOrder.HasValue)
            {   // Редактирование
                if (newStatusHistoryOrder > encloseEvent.StatusHistory.Count)
                {
                    ModelState.AddModelError("newStatusHistoryOrder", $"Значение должно быть не больше {encloseEvent.StatusHistory.Count}");
                    return Page();
                }

                if (statusHistoryOrder.Value != newStatusHistoryOrder - 1)
                {
                    encloseEvent.StatusHistory.RemoveAt(statusHistoryOrder.Value);
                    encloseEvent.StatusHistory.Insert(newStatusHistoryOrder - 1, historyRecord);
                }
                else
                  encloseEvent.StatusHistory[statusHistoryOrder.Value] = historyRecord;
            } else
            {   // Новая запись
                if (newStatusHistoryOrder > encloseEvent.StatusHistory.Count + 1)
                {
                    ModelState.AddModelError("newStatusHistoryOrder", $"Значение должно быть не больше {encloseEvent.StatusHistory.Count + 1}");
                    return Page();
                }
                encloseEvent.StatusHistory.Insert(newStatusHistoryOrder - 1, historyRecord);
            }

                
            if (_db.UpdateAsync(encloseEvent) == null)
                return RedirectToPage("/CainiaoStatuses/EncloseStatus", new { Encloseid = encloseId, EncloseOwnerId = encloseOwnerId });

            return RedirectToPage("/CainiaoStatuses/EncloseStatus", new { Encloseid = encloseId, EncloseOwnerId = encloseOwnerId });
        }

    }
}
