using Microsoft.AspNetCore.Mvc.Rendering;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Infrastructure
{
    public static class SelectListHelper
    {
        public static IList<SelectListItem> ConvertToSelectList<T>(IList<T> items, 
            string defaultText = "Select an Option") where T : ISelectable
        {
            var selectList = items.Select(item => new SelectListItem
            {
                Text = item.Name,  
                Value = item.Id.ToString()  
            }).ToList();

            selectList.Insert(0, new SelectListItem(defaultText, string.Empty));

            return selectList;
        }
    }
}
