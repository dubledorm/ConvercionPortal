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

        public void OnGet(int id, int ownerId)
        {
            ViewData["ActivePage"] = "ConvercionTypes";
            encloseAndCNStatus = _db.GetById(id, ownerId);
        }
    }
}
