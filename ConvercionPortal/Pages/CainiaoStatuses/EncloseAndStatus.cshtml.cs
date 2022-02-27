using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.CainiaoStatuses
{
    public class EncloseAndStatusModel : PageModel
    {
        private readonly IEncloseAndCNStatusRepository _db;
        public EncloseAndCNStatus? encloseAndCNStatus { get; set; }

        public EncloseAndStatusModel(IEncloseAndCNStatusRepository db)
        {
            _db = db;
        }

        public void OnGet(string separatedIdAndOwnerId)
        {   IdAndOwnerId idAndOwnerId = new IdAndOwnerId(separatedIdAndOwnerId); 

            ViewData["ActivePage"] = "EncloseAndStatuses";
            encloseAndCNStatus = _db.GetById(idAndOwnerId.Id, idAndOwnerId.OwnerId);
        }
    }
}
