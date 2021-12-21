using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.ConvercionTypes
{
    public class ConvercionTypeModel : PageModel
    {
        private readonly IConvercionTypeRepository _db;
        public ConvercionType? ConvercionType { get; set; }

        public ConvercionTypeModel(IConvercionTypeRepository db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            ViewData["ActivePage"] = "ConvercionTypes";
            ConvercionType = _db.GetById(id);
        }
    }
}
