using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ConvercionPortal.App_Code
{
    public class MenuHelper
    {
        public static string MenuItemClassName(ViewDataDictionary ViewData, string menuId)
        {
            if (ViewData["ActivePage"] == menuId)
                return "nav-link active";
            else
                return "nav-link";
        }
    }
}
