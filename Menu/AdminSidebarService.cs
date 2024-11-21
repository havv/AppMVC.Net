using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AppMvc.Net.Menu
{
    public class AdminSidebarService
    {
        private readonly IUrlHelper UrlHelper;
        public List<SidebarItem> Items { get; set; } = new List<SidebarItem>();

        public AdminSidebarService(IUrlHelperFactory factory, IActionContextAccessor action)
        {
            /*do IUrlHelper ko có trong  hệ thống depenency injection của ứng dụng  nên cần sử dụng IUrlHelperFactory và IActionContextAccessor (có dependency injection) 
            để tạo ra ( IActionContextAccessor cần thêm service trong Program.cs nhé)*/
            UrlHelper = factory.GetUrlHelper(action.ActionContext); 
            
            //Khởi tạo các mục sidebar
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.Divider

            });
             Items.Add(new SidebarItem(){
                Type = SidebarItemType.Heading,
                Title = "Quản lý chung"

            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.NavItem,
                Controller = "DbManage",
                Action = "Index",
                Area = "Database",
                Title = "Quản lý Database",
                AweSomeIcon = "fas fa-database"

            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.NavItem,
                Controller = "Contact",
                Action = "Index",
                Area = "Contact",
                Title = "Quản lý liên hệ",
                AweSomeIcon = "far fa-address-book"

            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.Divider

            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.NavItem,
                Title = "Phân quyền và thành viên",
                AweSomeIcon = "far fa-folder",
                collapseId = "role",
                Items = new List<SidebarItem>(){
                    new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Role",
                        Action = "Index",
                        Area = "Identity",
                        Title = "Các vai trò",

                    },
                     new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Role",
                        Action = "Create",
                        Area = "Identity",
                        Title = "Tạo role mới",

                    },
                     new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "User",
                        Action = "Index",
                        Area = "Identity",
                        Title = "Danh sách thành viên",

                    }
                }
                

            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.Divider

            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.NavItem,
                Title = "Quản lý bài viết",
                AweSomeIcon = "far fa-folder",
                collapseId = "blog",
                Items = new List<SidebarItem>(){
                    new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Category",
                        Action = "Index",
                        Area = "Blog",
                        Title = "Các chuyên mục",

                    },
                     new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Category",
                        Action = "Create",
                        Area = "Blog",
                        Title = "Tạo chuyên mục",

                    },
                     new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Post",
                        Action = "Index",
                        Area = "Blog",
                        Title = "Các bài viết",

                    },
                    new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Post",
                        Action = "Create",
                        Area = "Blog",
                        Title = "Tạo bài viết",

                    }
                }
            });
            Items.Add(new SidebarItem(){
                Type = SidebarItemType.NavItem,
                Title = "Quản lý sản phẩm",
                AweSomeIcon = "far fa-folder",
                collapseId = "product",
                Items = new List<SidebarItem>(){
                    new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "CategoryProduct",
                        Action = "Index",
                        Area = "Product",
                        Title = "Các chuyên mục",

                    },
                     new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "CategoryProduct",
                        Action = "Create",
                        Area = "Product",
                        Title = "Tạo chuyên mục",

                    },
                     new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Product",
                        Action = "Index",
                        Area = "Product",
                        Title = "Các sản phẩm",

                    },
                    new SidebarItem(){
                        Type = SidebarItemType.NavItem,
                        Controller = "Product",
                        Action = "Create",
                        Area = "Product",
                        Title = "Tạo sản phẩm",

                    }
                }
            });
        }

        public string renderHtml()
        {
            var html = new StringBuilder();
            foreach(var item in Items)
            {
                html.Append(item.RenderHtml(UrlHelper));
            }
            return html.ToString();
        }

        public void SetActive(string Controller, string Action, String Area)
        {
            foreach(var item in Items)
            {
                if(item.Controller == Controller && item.Action == Action && item.Area == Area)
                {
                    item.IsActive = true;
                    return;
                }
                else
                {
                    if(item.Items != null)
                    {
                        foreach(var childItem in item.Items)
                        {
                             if(childItem.Controller == Controller && childItem.Action == Action && childItem.Area == Area)
                            {
                                childItem.IsActive = true;
                                item.IsActive = true;
                                return;
                            }

                        }

                    }
                }
            }
        }


    }
}