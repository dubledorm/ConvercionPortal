using ConvercionPortal.Models;
using ConvercionPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConvercionPortal.Pages.ConvercionTypes
{
    public class EditModel : PageModel
    {
        private readonly IConvercionTypeRepository _db;

        [BindProperty]
        public ConvercionType ConvercionType { get; set; }


        public EditModel(IConvercionTypeRepository db)
        {
            _db = db;
        }


        public IActionResult OnGet(int? Id)
        {
            ViewData["ActivePage"] = "ConvercionTypes";
            ConvercionType? convercionType = null;
            if (Id.HasValue)
                convercionType = _db.GetById(Id.Value);
            else
                convercionType = new ConvercionType();

            if (convercionType == null)
                return RedirectToPage("/Error");
            ConvercionType = convercionType;
            return Page();
        }

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
    }
}
