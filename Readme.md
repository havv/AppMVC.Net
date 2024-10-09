## Controller
- La 1 lop ke thua tu lop controller : Microsoft.AspNetCore.Mvc
- Action trong controller la mot phuong thuc public (ko dc static)
- Action tra ve bat ky kieu du lieu nao, thuong la IActionResult
- Cac dich vu inject vao controller qua ham tao

## View
- La file .cshtml
- view cho action luu tai /Views/ControllerName/ActionName.cshmtl
- them thu muc luu tru view (them vao file Program)
 //{0} -> ten Action 
   //{1} -> ten Controller
   //{2} -> ten Area
   //RazorViewEngine.ViewExtension ~ cshtml
   options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

## Truyen du lieu sang View
-Model
-ViewData
-ViewBag
-TempData

# Day code len github
- tao 1 repository tren trang github cua ban
- trong visual studio code tren man hinh terminal thu hien cac lenh 
   + dotnet new gitignore de tao file .gitignore
   + git init để khởi tạo 1 kho chứa git
   + git add . đánh chỉ mục tất các các file cần theo dõi
   + git commit -m "Creat AppMVC"
   + git status (kiem tra trang thai hien tai nhanh hien tai dang la master)
   + git branch B1 (tao 1 nhanh moi B1) 
   + git branch (ktra xem co bao nhieu nhanh )
   + git remote add origin https://github.com/havv/AppMVC.Net.git (thực hiện liên kết kho chứa ở local và ở remote)
   + git push --all (đẩy toàn bộ các nhánh lên github)

# Cài đặt tool generator
 + dotnet tool install -g dotnet-aspnet-codegenerator (cài đặt công cụ codegenerator)
 + dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design (add package)
 + dotnet aspnet-codegenerator -h ,  dotnet aspnet-codegenerator controller -h (Gõ lệnh để xem hướng dẫn)
 + dotnet aspnet-codegenerator controller -name ProductController -namespace AppMvc.Net.Controllers -outDir Controllers (tạo controller)

# Area để thiết lập controller thuộc về 1 vùng nào đó
- tên area được dùng để thiết lập routing
- Là cấu trúc thư mục chứa MVC (dễ quản lý hơn)
- Thiết lập area cho controller  [Area("AreaName")]
- Tạo cấu trúc thư mục dotnet asp-codegenerator area Product

# Phát sinh các url (xem ví dụ trong file index của home)
-Url.ActionLink() Url.Action() sử dụng action để sinh ra url
- Url.RouteUrl() Url.Link() sử dụng tên route để sinh ra url
- <a> <form> <button>

# Tích hợp Entity Framework và các công cụ phát sinh code
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package MySql.Data.EntityFramework

# Docker với sql
- Chạy SQL Server trên docker chạy lệnh ở thư mục chứa docker-compose.yml
- chạy dịch vụ docker-compose up -d
- khởi động nếu dịch vụ bị dừng docker-compose start
- xóa dịch vụ docker-compose down 
- ktra container đang chạy docker ps

# Dùng migration để tạo cơ sở dữ liệu
- dotnet ef migrations add Init (tạo migration đầu tiên)
- dotnet ef database update
- Xem thêm code trong thư mục Areas/Database

# Thiết lập cấu hình log information khi chạy ứng dụng sẽ xuất hiện thông tin trên màn hình terminal
- Thêm các thông tin muốn log vào file appsetting.json 
 + ví dụ "Microsof.EntityFrameworkCore.Query" : "Information"
         "Microsof.EntityFrameworkCore.Database.Command" : "Information"
   tham khảo thêm xem thêm thông tin simple logging entity framework Message categories

# Chuyển file scss sang css
 - Ktra đã cài node chưa node -v
 - gõ lệnh npm init để tạo file package
 - cài các package của node 
  + npm install --global gulp-cli
  + npm install gulp
  + npm install node-sass postcss sass
  + npm install gulp-sass gulp-less gulp-concat gulp-cssmin gulp-uglify rimraf gulp-postcss gulp-rename
  + Vắn tắt ý nghĩa của các gói trên

gulp Task Runner
node-sass biên dịch sass (.scss)
rimraf sử dụng để thi hành tác vụ xóa file
gulp-concat nối các file thành 1
gulp-cssmin tối ưu cơ file .CSS (xóa bỏ các thành phần thừa)
gulp-uglify tối ưu cơ file .JS (không dùng phần này)
gulp-postcss
+ chạy lệnh gulp nameTask để chạy tác vụ chuyển scss -> css vd gulp default
+ thêm task watch để load mỗi lần thay đổi file scss sau đó gọi gulp watch (xem file gulpfile.js)

# Tích hợp Identity vào Asp.Net MVC
dotnet add package System.Data.SqlClient
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Logging.Console

dotnet add package Microsoft.AspNetCore.Identity
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.AspNetCore.Identity.UI
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Http.Abstractions
dotnet add package Microsoft.AspNetCore.Authentication.Cookies
dotnet add package Microsoft.AspNetCore.Authentication.Facebook
dotnet add package Microsoft.AspNetCore.Authentication.Google
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount
dotnet add package Microsoft.AspNetCore.Authentication.oAuth
dotnet add package Microsoft.AspNetCore.Authentication.OpenIDConnect
dotnet add package Microsoft.AspNetCore.Authentication.Twitter

dotnet add package MailKit
dotnet add package MimeKit

// Dang ky Identity
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
- copy source tu xuanthulab ve 
- Có file libman.json -> thực hiện lệnh libman restore để tải thư viện multiple-select về lưu ở thư mục wwwroot ( Để sử dụng đc lệnh libman cần cài đặt dotnet tool install -g Microsoft.Web.LibraryManager.Cli )
- copy mailsetting trong file appsetting.identity.json vào file appsetting.json sau đó xóa file appsetting.identity.json đi
- Thiet lap policy (xem trong file program)
 //Thiet lap policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(RoleName.Administrator);
    });

});
- Sử dụng policy xem trong file _MenuManagePartial.cshtml
# Thiết lập giá trị các phần từ cho tag select 
- Các phần tử của tag select là 1 collection SelectListItem (xem file create.cshtml của blog)
@{
  var items = new List<SelectListItem>();
        items.Add(new SelectListItem(){
            Text = "Mục 1",
            Value = "gt1"

        });
         items.Add(new SelectListItem(){
            Text = "Mục 2",
            Value = "2",
            Selected = true

        });
         items.Add(new SelectListItem(){
            Text = "Mục 3",
            Value = "3",
            Disabled = true

        });
        hoặc 
          var data = new object[]
        {
            new {
                ten = "ten1",
                giatri =1 
            },
             new {
                ten = "ten2",
                giatri = 2 
            },
             new {
                ten = "ten3",
                giatri = 3
            }

        };
        var items = new SelectList(data, "giatri","ten", 2);
}
<select asp-items = "items"></select>

# Seed data
- dotnet add package bogus
- Xem file DbManageController

# phan slug cua them bai post 
- chua lam dc phan phat sinh slug tu dong. Do slug la require (xem lai sau xem file AppUtilities.cs...)