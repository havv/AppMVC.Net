using AppMvc.Net.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Net.Components;

[ViewComponent]
public class CategoryProductSidebar : ViewComponent
{
    public class CategorySidebarData
    {
        public List<CategoryProduct> Categories { get; set; }
        public int Level { get; set; }
        public string CategorySlug { get; set; }
    }
    public IViewComponentResult Invoke(CategorySidebarData data)
    {
        return View(data); //gọi đến default.cshtml
    }


}