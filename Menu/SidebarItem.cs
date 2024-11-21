using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Net.Menu
{
    public enum SidebarItemType
    {
        Divider,
        Heading,
        NavItem
    }
    public class SidebarItem
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public SidebarItemType Type { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }

        public string Area { get; set; }

        public string AweSomeIcon { get; set; }

        public List<SidebarItem> Items { get; set; }

        public string collapseId { get; set; }

        public string GetLink(IUrlHelper urlHelper)
        {
            return urlHelper.Action(Action, Controller, new { area = Area });
        }
        public string RenderHtml(IUrlHelper urlHelper)
        {
            var html = new StringBuilder();
            if (Type == SidebarItemType.Divider)
            {
                html.Append("<hr class=\"sidebar-divider\">");
            }
            else if (Type == SidebarItemType.Heading)
            {
                html.Append(@$"<div class=""sidebar-heading"">
                                        {Title}
                                </div>");

            }
            else if (Type == SidebarItemType.NavItem)
            {
                if (Items == null)
                {
                    var url = GetLink(urlHelper);
                    var icon = (AweSomeIcon != null) ? $"<i class=\"{AweSomeIcon}\"></i>" : "";
                    var cssClass = "nav-item";
                    if (IsActive) cssClass += " active";
                    html.Append(@$" <li class=""{cssClass}"">
                                        <a class=""nav-link"" href=""{url}"">
                                            {icon}
                                            <span>{Title}</span></a>
                                     </li>");
                }
                else
                {
                    var cssClass = "nav-item";
                    var collapseCss = "collapse";

                    if (IsActive)
                    {
                        cssClass += " active";
                        collapseCss += " show";
                    } 
                    var icon = (AweSomeIcon != null) ? $"<i class=\"{AweSomeIcon}\"></i>" : "";
                    var itemMenu = "";
                    foreach(var item in Items)
                    {
                        var urlItem = item.GetLink(urlHelper);
                        var cssItem = "collapse-item";
                        if(item.IsActive) cssItem+= " active";
                        itemMenu += $"<a class=\"{cssItem}\" href=\"{urlItem}\">{item.Title}</a>";
                    }

                    html.Append(@$"<li class=""{cssClass}"">
                                    <a class=""nav-link collapsed"" href=""#"" data-bs-toggle=""collapse"" data-bs-target=""#{collapseId}""
                                            aria-expanded=""true"" aria-controls=""{collapseId}"">
                                        {icon}
                                        <span>{Title}</span>
                                    </a>
                                    <div id=""{collapseId}"" class=""{collapseCss}"" aria-labelledby=""headingTwo"" data-parent=""#accordionSidebar"">
                                         <div class=""bg-white py-2 collapse-inner rounded"">
                                            {itemMenu}
                                        </div>
                                    </div>
                                 </li>");
                }
            }
            return html.ToString();
        }


    }
}