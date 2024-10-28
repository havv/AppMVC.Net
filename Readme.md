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

# Tích hợp html editor
- Có nhiều trình soạn thảo html editor : fckeditor, summbernote,.. (thư viện javascript)
- Sử dụng libman để lấy về thư viện summernote 
- Trong file libman hiện đang thiết lập lất về từ cdnjs (có thể vào trang cdnjs tìm summernote lấy tên và phiên bản về copy vào file libman)
    {
      "library": "summernote@0.9.0",
      "destination": "wwwroot/lib/summernote"
    }
- Thực hiện lệnh libman restore để tự động lấy thư viện summernote về và lưu trong thư mục wwwroot như cấu hình trong file libman
- Xem tài liệu trên trang summernote.org
- Phần thư viện  <script src="~/lib/summernote/summernote-bs5.min.js"></script>
<link rel="stylesheet" href="~/lib/summernote/summernote-bs5.min.css">
    } phải đặt sau thư viện jquery
   css, js của summnernote có thể cho vào 1 section Script 
   hoặc phải để jquery vào trong thẻ head để xuất hiện trc summernote
   (chi tiết xem file index và _layout)

- Để sử dụng đc nhiều lần tạo ra 1 partial summernote.cshtml 
(xem file summernote.cshtml  và summernote.cs)

# Tích hợp trình quản lý file eFinder 
- Đây là 1 thư viện phía client, nó sử dụng javascript jquery để tạo ra giao diện tương tác với hệ thống file trên ứng dụng
- Tìm github của efinder đọc phần wiki https://github.com/Studio-42/elFinder/wiki
- Ứng dụng của chúng ta phải cung cấp các api Client server api có các command list như open, file ... gọi chung là connector (chi tiết xem phần Client server apitrong tài liệu)
- Để xây dựng đc các api này thì sẽ rất lâu nên sẽ dùng 1 thư viện của bên thứ 3 là elFinder.NetCore https://github.com/gordon-matt/elFinder.NetCore(chúng ta chỉ cần tích hợp thư viện này vào dự án)
 có thể sử dụng lệnh dotnet add package elFinder.NetCore --version 1.4.0 để lấy thư viện về
 hoặc như trong project này thì dùng libman
thêm  {
      "library": "jqueryui@1.12.1",
      "destination": "wwwroot/lib/jqueryui"
    },
    {
      "library": "elfinder@2.1.57",
      "destination": "wwwroot/lib/elfinder"
    }
    vào file libman rồi gọi lệnh libman restore
    thực hiện update jqueryui và elfinder gọi lệnh libman update jqueryui
                                                    libman update elfinder
- Tạo area file dotnet aspnet-codegenerator area Files
- Tạo FileController dotnet aspnet-codegenerator controller -name FileManagerController -outDir Areas/Files/Controllers/
- Nạp các thư viện cần thiết vào thẻ head của layout
 <link rel="stylesheet" href="~/lib/jqueryui/themes/base/theme.css" />
    <link rel="stylesheet" href="~/lib/jqueryui/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="~/lib/elfinder/css/elfinder.full.css" />
        <link rel="stylesheet" href="~/lib/elfinder/css/theme.min.css" />
     <script src="~/lib/jqueryui/jquery-ui.min.js"></script>
    <script src="~/lib/elfinder/js/elfinder.full.js"></script>

- Tích hợp elFinder.NetCore vào dự án  dotnet add package elFinder.NetCore
- Copy code mẫu trên xuanthulab vào file FileManagerController
- tạo thư mục files trong wwwroot
- Nếu muốn file upload đc lưu trong 1 thư mục riêng là Uploads chứ k phải thư mục files trong wwwroot thì để truy cập đc file tĩnh bên trong thư mục Uploads thì phải cho vào pipeline 1 staticfile
 + Trong file Program.cs thêm
  //Thiết lập truy cập file tĩnh lưu trong thư mục Uploads
app.UseStaticFiles(new StaticFileOptions(){
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/contents"

});
 + xóa thư mục files trong wwwroot. Truy cập file trong thư mục Uploads qua đường dẫn /contents/file
 - Cấu hình để elfinder làm việc với Uploads (xem hàm GetConnector trong file FileManagerController )
 - Thêm elfinder vào summernote
 + nạp thư viện elfinder vào _Summernote.cshtml
  <link rel="stylesheet" href="~/lib/jqueryui/themes/base/theme.css" />
    <link rel="stylesheet" href="~/lib/jqueryui/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="~/lib/elfinder/css/elfinder.full.css" />
    <link rel="stylesheet" href="~/lib/elfinder/css/theme.min.css" />
    <script src="~/lib/jqueryui/jquery-ui.min.js"></script>
    <script src="~/lib/elfinder/js/elfinder.full.js"></script>
    + viết plugin cho summernote để có nút bấm elfinder xem trong scripts của file _Summernote.cshtml
# Nâng cấp phiên bản net 
- kiểm tra sdk đang cài trên máy bằng lệnh 
+ dotnet --list-sdks
+ dotnet --list-runtime
+ dotnet --version
- nếu chưa có phiên bản sdk cần nâng cấp thì tải về cài đặt 
- Sửa TargetFramework trong file csproj
- nâng cấp các package trong file csproj lên phiên bản mới nhất (vd dotnet add package Bogus làm tương tự với các package khác)
- Để làm nhanh thì làm như sau 
  + liệt kê tất các các package dùng trong dự án dotnet list package copy rồi chỉnh sửa paste lại vào terminal cho chạy 
  + Chạy xong chạy dotnet restore xem còn package nào chưa đc update 
# Một số vấn đề cần lưu ý khi viết query 
 nên viết như này nhé
            var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory)
                        .Include(c => c.CategoryChildren)
                        .Where(c => c.ParentCategory == null);
            var categories = (await query.ToListAsync()).ToList();
Không nên viết như này vì như này thì sẽ select hết data sau đó mới thực hiện where => ảnh hưởng performance (ko nên làm theo xuanthu như này nhé)
             var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory)
                        .Include(c => c.CategoryChildren);
                       
            var categories = (await query.ToListAsync())
             .Where(c => c.ParentCategory == null).ToList();
# Xây dựng trang hiển thị tin tức blog
 - Tạo controller dotnet aspnet-codegenerator controller -name ViewPostController -namespace AppMvc.Net.Areas.Blog.Controllers -outDir Areas/Blog/Controllers/
 - Tạo view Index, _LayoutBlog ...
 - Tạo ra 1 component để render ra menu 
  + Trong thư mục shared tạo ra 1 thư mục Components/CategorySidebar
  + Tạo class CategorySidebar kế thừa ViewComponent, tạo file view defautl.cshtml gọi từ Invoke trong CategorySidebar
  + Để sử dụng component CategorySidebar trong cshtml khác thì gọi như sau 
    @await Component.InvokeAsync("AppMvc.Net.Components.CategorySidebar") (xem file index cua Post )
  + Tạo 1 lớp con CategorySidebarData  bên trong CategorySidebar (xem file CategorySidebar)
  # Một số lưu ý 
  - Muốn sử dụng ThenInclude thì dùng thư viện using Microsoft.EntityFrameworkCore chứ k phải dùng using System.Data.Entity; (xem file ViewPostController)
  - Trong file csproj có cả package <PackageReference Include="MySql.Data.EntityFramework" Version="9.0.0" /> nên khi buid có waring warning NU1701: Package 'MySql.Data.EntityFramework 9.0.0' was restored using '.NETFramework,Version=v4.6.1, .NETFramework,Version=v4.6.2, .NETFramework,Version=v4.7, .NETFramework
,Version=v4.7.1, .NETFramework,Version=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework 'net8.0'. This package may not be fully compatible with your project.
 => ko dùng thì bỏ đi nhé
- git status : ktra xem dang o nhanh nao, cac file thay doi cac file moi
- git add . : add tat ca cac file thay doi , them ..
- git commit -m "ViewPost"
- git checkout master : chuyen sang nhanh master
- git merge B10 : gop nhanh B10 vao master
- git push --all : day toan bo ket qua len github

