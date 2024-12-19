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
 hoặc như trong project này thì dùng libman ( Neu chua co libman thi dung lenh sau de cai dat dotnet tool install -g Microsoft.Web.LibraryManager.Cli de su dung libman xem tren trang microsoft nhe)
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
- git branch -r : liet ke cac nhanh o tren git git branch : liet ke cac nhanh tren local
- git checkout B10: chuyen sang nhanh B10
- git branch B11: tao nhanh B11
- git checkout B11: chuyen sang nhanh B11
# Xây dựng trang quản lý sản phẩm
- Tạo các table liên quan đến product
- Thêm các table vào AppDbContext 
- Dùng entity framework migration để update database : dotnet ef migrations add Product ,  dotnet ef database update
- Thêm data fake cho Product (xem file DbManageController)
# Xây dựng upload photo cho sản phẩm
- Tạo model ProductPhoto, cập nhật dbcontext => cập nhật database dùng migrations
- Tao file UploadPhoto.cshtml
- Tạo 1 class UploadOneFile có chứa IFormFile để upload (xem file ProductController)
- Sử dụng ajax gọi api ListPhotos để hiển thị dữ liệu photo thay vì sử dụng view của asp.net (xem file UploadPhoto.cshtml)
- Them upload vao edit san pham
- can xem lai sao moi lan reaload trang la lai upload lai anh cu ???
# Xay dung trang hien thi san pham va dat hang
- Lam tuong tu phan Blog cua post (copy sang)
- Chuc nang dat hang
- Để xây dựng chức năng giỏ hàng, danh sách các mặt hàng sẽ lưu trong Session của hệ thống. Do vậy, ứng dụng cần đảm bảo kích hoạt Session - làm theo hướng dẫn tại Sử dụng Session trong ASP.NET , đồng thời cũng dùng kỹ thuật JSON để lưu dữ liệu nên cần đảm bảo tích hợp hai gói là:
dotnet add package Newtonsoft.Json (convert 1 đối tượng thành json sau đó lưu vào session , chuyển chuỗi json thành 1 đối tượng)
dotnet add package Microsoft.AspNetCore.Session (Kích hoạt session)
dotnet add package Microsoft.Extensions.Caching.Memory (Lưu trữ thông tin session ở trong bộ nhớ)
- Thêm vảo Startup.ConfigureServices (Đối với .net cũ)

services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "xuanthulab";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0,30, 0);    // Thời gian tồn tại của Session
});
Trong Startup.Configure cho thêm vào middleware UseSession (sau UseStaticFiles()):

app.UseSession();
- Với .net core thêm vào file Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
builder.Services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "appmvc";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0,30, 0);    // Thời gian tồn tại của Session
});
app.UseSession();  // (sau UseStaticFiles()):
//Session có thể truy cập thông qua đối tượng HttpContext trong controller hoặc trong view

- Tạo các file CartItem, CartService (Đăng ký dịch vụ CartService services.AddTransient<CartService>() trong file Program.cs) 
- Trong ViewProductController inject dịch vụ CartService để sử dụng
- Xây dựng chức năng thêm mặt hàng vào cart (xem file ViewProductController)
- Tạo partial _CartPartial.cshtml để hiển thị giỏ hàng
- Mở layout ra, chèn đoạn mã để render partial này tại menu @await Html.PartialAsync("_CartPartial") (Lúc nào thì dùng Html.PartialAsync , luc nao thi dung thẳng thẻ <partial name ="">?? )
- Xây dựng chức năng xóa giỏ hàng, update giỏ hàng, gửi đơn hàng (xem file ViewProductController)

# Tùy biến giao diện của trang quản trị sử dụng template mẫu SB Admin (giao diện mẫu dành cho các trang quản trị)
- SB Admin 2 có sử dụng các thành phần là Bootstrap 4, jQuery, chart.js, fontawesome, jquery-easing. Nên cần đảm bảo có các thành phần này trong dự án.
- Tải mã nguồn SBAdmin về 
- Với Bootstrap và jQuery mặc định tích hợp trong ASP.NET mẫu đã xây dựng, chart.js ở đây không dùng đến nên chưa cần tích hợp. Còn lại Font Awesome và jQuery-easinng ta tích hợp vào dự án bằng LibMan
Ktra version jquery-easing bằng cách tìm kiếm trên cdnjs.com
ktra version font-awesome sử dụng trong source sb admin vendor\fontawesome-free\attribution.js
Thêm code vào file libman.json
 {
      "library": "jquery-easing@1.4.1",
      "destination": "wwwroot/lib/jquery-easing"
    },
    {
      "library": "font-awesomeg@5.15.3",
      "destination": "wwwroot/lib/font-awesome"
    }
Thực hiện lệnh sau:
libman restore

Hoặc có thể dùng lệnh  nếu ko sửa file libman.json
libman install jquery-easing
libman install font-awesome
- libman bị lỗi ko install đc font-awesome (đã thử libman cache clean libman restore mà vẫn k đc)
 => dùng npm để download và dùng thêm gulp để copy sang thư mục wwwroot ( đọc thêm ở link này https://stackoverflow.com/questions/49552235/how-to-install-font-awesome-in-visual-studio-2017-using-asp-net-core-v2)
 - dùng npm vẫn ko cài đc font-awesome 5 trở lên. Ktra version bằng lệnh  npm show font-awesome versions thì 4.7.0 là max => cài 4.7.0
 - copy font-awesome trong node_modules(sau khi cài bằng npm thì font-awesome ở trong thư mục node_modules) bằng gulp (xem file gulp) chạy lệnh gulp lib để copy nhé 
 - gulp 5 ko dùng đc ** trong gulp.js nên đổi thành gulp 4 (npm install gulp@4) (chưa hiểu sao ??) chạy lại gulp lib sau khi cài lại để copy font-awesome
 - font-awesome version 4 thiếu nhiều icon nên cài version 5 (có vẻ từ version 5 sẽ phải cài theo kiểu  "@fortawesome/fontawesome-free": "^5.15.3" thêm dòng này vào package.json bỏ dòng "fontawesome": "^4.7.0" rồi chạy lệnh npm install nhé , có thể gỡ bản version 4 bằng cách npm uninstall font-awesome@4.7.0)
 - Download mã nguồn sb admin Sửa các trang bằng cách copy source trong sb admin  và sửa cshtml 
 - Trong trang quản trị có nút bấm ở sidebar để nó hoạt động thì đặt file js xuống dưới cùng file cshtml nhé. (xem file _LayoutAdmin.cshtml)
 - Các mục của sidebar sẽ do view đổ sang nên từ dòng 52 trong file _LayoutAdmin.cshtml sẽ cho vào 1 chỉ thị @RenderSectionAsync("Sidebar", false) xuất ra nội dung của section Sidenar
 - @$ trong string la string viết đc trên nhiều dòng và có kèm giá trị
 - Chú ý từ bootstrap 5 thì data-toggle="collapse" data-target="#collapseTwo" đổi thành data-bs-toggle="collapse" data-bs-target="#collapseTwo"

 # Publish ứng dụng , triển khai chạy ứng dụng với Nginx, Htpp Apache
 - publish ứng dụng dotnet publish -c Release -o app/publish Kết quả là xuất ra ứng dụng ra thư mục app/publish, bạn dùng thư mục này để phân phối - triển khai chạy ứng dụng. 
   Trong thư mục có file dll tên ứng dụng dùng để chạy ứng dụng
   dotnet tên-ứng-dụng.dll
  - Trong dự án có thể có các thư mục tài nguyên, ví dụ thư mục Uploads, nếu muốn thư mục này được copy vào publish thì trong file: .csproj thêm vào đoạn mã:

  <ItemGroup> 
    <Content Include="Uploads\**"> 
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory> 
    </Content> 
  </ItemGroup>

  - Khi chạy ứng dụng bằng lệnh dotnet tên-ứng-dụng.dll thì có 1 máy chủ tích hợp sẵn ở trong aspnet là kestrel server lắng nghe ở cổng 5001
  - kestrel server lắng nghe request http gửi đến. Mọi request sẽ đi qua kestrel server phân tích gửi đến ứng dụng của chúng ta 
  - Tuy nhiên khi triển khai ở môi trường product, ứng dụng chạy thực tế thì thường sử dụng 1 máy chủ http chuyên nghiệp(Apache, Ngnix)(reverse proxy server) sau đó mới đến kestrel sau đó đến ứng dụng 
  - Thường kestrel server chỉ cấu hình http còn proxy server (Apache, Ngnix) sẽ cấu hình https
  - Với .net cũ thêm vào file Program.cs : 
  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            //webBuilder.UseUrls("http://0.0.0.0:8090", "https://0.0.0.0:8091");
            webBuilder.UseUrls("http://0.0.0.0:8090");
            webBuilder.UseStartup<Startup>();
        });
  - Với .net mới thêm vào  file Program.cs :
  builder.WebHost.UseUrls("http://localhost:8090");
  Hoặc dùng 
  app.Run("http://localhost:8090");
  - Sửa lại trang home và trang database manage 1 chút
  - Tạo ra máy ảo vps chạy hệ điều hành centos, trên đó sẽ cài Apache Ngnix Sql server
  - Cài đặt virtual box
  - Bạn có thể tạo trực tiếp máy ảo từ virtual box hoặc dùng công cụ vagrant tự động hóa quá trình tạo ra máy ảo
  - Tạo thư mục Vagrant -> tạo file VagrantFile -> vào thư mục vagrant trong cửa sổ terminal thực hiện lệnh vagrant up 
  => tự động tạo máy ảo như file cấu hình VagrantFile (Lưu ý cài máy ảo trc khi chạy lệnh)
  - Xóa máy ảo vagrant destroy
  - SSh vào máy ảo bằng lệnh vagrant ssh
  - Tạo ra file gồm các lệnh shell để cài đặt các ứng dụng vào máy ảo
  - Chạy lệnh vargrant up để tạo máy ảo vào cài các ứng dụng 
  - Ở cửa sổ terminal của máy host ssh vào máy ảo ssh root@192.168.10.99 theo địa chỉ ip và password đã đc thiết lập trong file vagrantfile
  - Chú ý sau khi destroy máy ảo vào cài lại rồi ssh vào máy ảo thì bị lỗi REMOTE HOST IDENTIFICATION HAS CHANGED! 
    thì thực hiện lệnh ssh-keygen -R <host> ví dụ ssh-keygen -R 192.168.10.99 để xóa keys cũ đi
    ( -R hostname Removes all keys belonging to hostname from a known_hosts file. This option is useful to delete hashed hosts (see the -H option above).)
    - Ktra internet trên máy ảo  ping -c 6 www.google.com, wget www.google.com, curl www.google.com
    - Trong khi cài đặt nếu bị lỗi Could not resolve host: mirrorlist.centos.org; Unknown error thì : 
    
Could not resolve host: mirrorlist.centos.org; Unknown error
This indicates that you either (a) don't have a properly configured DNS server or (b) your network configuration isn't correct and you can't connect to a DNS server to check the hostname mirrorlist.centos.org.

Try using ping 8.8.8.8. If this fails, try ping <local-gateway-ip>. If that also fails, your local network configuration is wrong and you'll have to check the configuration.

If you can ping 8.8.8.8, try using host, nslookup or dig to check the DNS settings like host google.com or dig google.com. If these fail, you need to check your DNS settings. Check /etc/resolv.conf to see what's configured.

UPDATE

Since /etc/resolv.conf is blank, you need to setup a DNS resolver. I would suggest entering the following into the file using nano or vi (or whatever your comfortable using):

nameserver 9.9.9.9

Save this file, then try yum update again.(Cách này ko đc xem cách dưới)

You can also try other DNS hosts if you would rather, such as 8.8.8.8 or 8.8.4.4 or any of the OpenDNS hosts.

Background
Since CentOS 7 has reached EOL, the mirror is moved to vault. When yum is executed, it will return an error "Cannot find a valid baseurl for repo: base/7/x86_64".

Solution
We need to update CentOS-Base.repo in /etc/yum.repos.d/CentOS-Base.repo

Uncomment the lines starting with baseurl.

Update all http://mirrorlist.centos.org to http://vault.centos.org.

Update all http://mirror.centos.org to http://vault.centos.org.

We can clear the cache with sudo yum clean all.
Làm theo cách này chạy đc yum update nhưng k cài đc dotnet nên đổi sang cài centos 9 như trong file VagrantFile
- ko cài đc mssql2017 nên đổi sang mssql2022
- khi cài apache theo lệnh sudo yum -y install httpd mod_ssl mod_rewrite
thì gặp lỗi sau
No match for argument: mod_rewrite
Error: Unable to find a match: mod_rewrite
nên bỏ mode_rewite đi nhé (chưa hiểu sao thêm 2 cái mod vào đọc lại sau)
- Ktra các thông tin sau nhé dotnet --version systemctl status httpd, systemctl status nginx, systemctl status mssql-server
- Thiết lập các cài đặt cho sql server password ...
 + cấu hình phiên bản, password sa : sudo /opt/mssql/bin/mssql-conf setup 
  + Sau khi thực hiện lệnh nó hiển thị các lựa chọn, mình sẽ chọn phiên bản express free
  + Khi chọn express xong nó chạy sẽ hiển thị lỗi gì utf8(cần thiết lập biết môi trường export gì đó ??) -> ko thiết lập biến môi trường tắt may bật lại thì ko bị nữa ???
- Để chạy máy chủ apache hay dịch vụ httpd thực hiện 2 lệnh sau : systemctl enable httpd systemctl start httpd
- Luu y Xuất hiện lỗi sau khi vagrant destroy và vagrant up lại VBoxManage.exe: error: Could not rename the directory 'C:\Users\Hai\VirtualBox VMs\generic-centos9s-virtualbox-x64_1733114468130_76088' to 'C:\Users\Hai\VirtualBox VMs\aspapp' 
to save the settings file (VERR_ALREADY_EXISTS). Sau đó xóa thư mục như dưới đây 
1) I had to manually delete C:\Users\My_name\VirtualBox VMs\machine_name folder.
2) To prevent this from happening again, before 'vagrant destroy' command I always stop current machine with 'vagrant suspend'.
thì bị lỗi Your VM has become "inaccessible."  => vào virtualbox xóa máy ảo tương ứng bị lỗi k xóa đc sau đó vagrant up lại bình thường
- Truy cập ứng dụng từ máy host bằng địa chỉ 192.168.56.99 : 80 (Phải stop hoặc disable firewall mới gọi đc systemctl stop firewalld systemctl disable firewalld)
- Tạo user mới bằng lệnh useradd aspnet. Nó sẽ tạo 1 thư mục aspnet ở trong /home (ktra bằng lệnh ls /home )
- Vào thư mục home cd /home (Gõ lệnh ls -la để xem thư mục aspnet thuộc sở hữu của user aspnet)
- Thiết lập user cho aspnet: passwd aspnet 
- Giờ sẽ copy thư mục app/publish vào thư mục aspnet của máy ảo để chạy: Từ terminal của thư mục aspmvc.net thực hiện lệnh sau 
scp -r app/publish aspnet@192.168.56.99:/home/aspnet
- Vào thư mục aspnet trong máy ảo chạy lệnh dotnet AppMvc.Net.dll để chạy ứng dụng trên máy chủ kestrel của máy ảo
- Có thể cài đặt thêm SqlCmd trong máy ảo để tương tác với sql nhé (Xem ở link https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-setup-tools?view=sql-server-ver16&tabs=redhat-install)
 Install the SQL Server command-line tools sqlcmd 
 For Red Hat 9, use the following command:
 1. Download the Microsoft Red Hat repository configuration file.
curl https://packages.microsoft.com/config/rhel/9/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo
2. If you had a previous version of mssql-tools installed, remove any older unixODBC packages.

sudo yum remove mssql-tools unixODBC-utf16 unixODBC-utf16-devel
3. Run the following commands to install mssql-tools18 with the unixODBC developer package.

sudo yum install -y mssql-tools18 unixODBC-devel
To update to the latest version of mssql-tools, run the following commands:

sudo yum check-update
sudo yum update mssql-tools18
4. Optional: Add /opt/mssql-tools18/bin/ to your PATH environment variable in a bash shell.

To make sqlcmd and bcp accessible from the bash shell for login sessions, modify your PATH in the ~/.bash_profile file with the following command:
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bash_profile
source ~/.bash_profile
To make sqlcmd and bcp accessible from the bash shell for interactive/non-login sessions, modify the PATH in the ~/.bashrc file with the following command:
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
- Check version sqlcmd sqlcmd "-?"

- Login sql : sqlcmd -U sa -P Password123 Nhưng sau khi dùng lệnh này thì bị lỗi Sqlcmd: Error: Microsoft ODBC Driver 18 for SQL Server : SSL Provider: [error:0A000086:SSL routines::certificate verify failed:self-signed certificate].
=> Thêm tham số -C thì đc sqlcmd -U sa -P Password123 -C
 - List cac database dung lenh select name from sys.databases go
 - Xoa database drop database appmvc go
 - Co loi khi seed data lan dau xem lai sau nhe??
 - Khi chạy trực tiếp bằng lệnh dotnet app.dll thì khi ứng dụng bị lỗi crash thì phải chạy lại 1 cách thủ công => tạo 1 dịch vụ để mỗi lần ứng dụng crash nó tự chạy lại 
 Tạo ra file dịch vụ trong thư mục /etc/systemd/system/, ví dụ ứng dụng là mvcblog, tạo ra file /etc/systemd/system/mvcblog.service (dùng lệnh vi), sau đó biên tập nội dung file này như sau:

[Unit]
Description=Ung dung ASP.NET MVC BLOG

[Service]
WorkingDirectory=/home/userasp/mvcblog
ExecStart=/usr/bin/dotnet /home/userasp/mvcblog/mvcblog.dll
Restart=always
# Khởi động lại ứng dụng sau 10 bị crash
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=asp-net-app
User=userasp
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
/usr/bin/dotnet là đường dẫn đầy đủ đến file binary của lệnh dotnet, có thể kiểm tra bằng lệnh

which dotnet
Để thiết lập dịch vụ tự động chạy bạn

systemctl enable mvcblog
Khởi chạy dịch vụ (ứng dụng)

systemctl start mvcblog
Xem trang thái

systemctl status mvcblog

liệt kê các ứng dụng đang chạy 
systemctl list-units --type service

Lưu ý, ứng dụng ASP.NET chạy trên lắng nghe ở cổng 8090 hoặc cổng do bạn thiết lập, cổng này không public ra ngoài (không dùng firewall để mở). Cổng chỉ dùng cho các dịch vụ như apache, nginx chuyển hướng đến.
- Các lệnh với firewall (xem file firewall)
- Cấu hình máy chủ apache để truy cập ở cổng 80 sẽ tương ứng với ứng dụng này ở cổng 8090 của máy chủ kestrel
- Xác định file cấu hình của apache ở đâu thực hiện lệnh httpd -V ta sẽ thấy thư mục gốc nằm ở /ect/httpd , thư mục config sẽ nằm ở /etc/httpd/conf/httpd.conf
- Có thể sửa file cấu hình trên máy ảo bằng cách dùng vi /etc/httpd/conf/httpd.conf. Hoặc dùng extention remote ssh trên visual code
- Cài extention ssh : vào extentions tìm remote ssh để install. Sau đó vào View -> Command Palete.. -> gõ remote ssh ->  remote ssh connect to host -> add new host -> gõ địa chỉ root@192.168.56.99 -> Chọn file cấu hình ssh config lưu -> connect -> cửa sổ visual code mới đc mở -> chọn platform linux -> nhập password
-> open folder -> nhập / để mở toàn bộ hệ thống file trên máy chỉ 192.168.56.99
- Trong file httpd.conf có chỉ thị IncludeOptional conf.d/*.conf nghĩa là nó include tất cả các file có đuôi conf trong thư mục conf.d
-> để config httpd có thể sửa trong file httpd.conf hoặc các file trong thư mục conf.d
- Ở đây sẽ sửa trực tiếp file httpd.conf (xem file Cài đặt và cấu hình Virtual Host cho máy chủ Apache)
- với ứng dụng đang làm máy chủ Apache làm việc như 1 proxy nó sẽ chuyển hướng giao thức http đến ứng dụng đang chạy dotnet thì chúng ta phải cho vào cấu hình :
Thiết lập Apache như là một Proxy, chuyển giao thức Http đến một cổng khác đang có địch vụ http chạy. Ví dụ như ứng dụng ASP.NET đang chạy và lắng nghe ở cổng 5000
thiết lập header
<VirtualHost *:*>
    RequestHeader set "X-Forwarded-Proto" expr="%{REQUEST_SCHEME}e"
</VirtualHost>
Sau đó tạo khối VirtualHost lắng nghe ở cổng 80 và thiết lập Apache như 1 proxy. 
Các thông điệp http mà Apache nhận đc sẽ đc chuyển đến cổng 80
Cấu hình địa chỉ domain sử dụng ServerName,ServerAlias
<VirtualHost *:80>
    ServerName appmvc.net
    ServerAlias *.appmvc.net #(*.appmvc.net nghĩa là truy cập bằng www.appmvc.net hoặc abc.appmvc.net .. đều đc)
    ErrorLog ${APACHE_LOG_DIR}domain.net-error.log
    CustomLog ${APACHE_LOG_DIR}domain.net-access.log common
    ProxyPreserveHost On
    ProxyPass / http://127.0.0.1:5000/
    ProxyPassReverse / http://127.0.0.1:5000/
</VirtualHost>
- Thiết lập xong thì khởi động lại Apache
systemctl restart httpd

- Do chưa có tên miền nên ở đây mình sẽ tạo ra 1 địa chỉ tên miền ảo khi truy cập địa chỉ appmvc.net thì tương ứng trỏ đến server 192.168.56.99 mở file host của hệ thống để cập nhật (ở window thì là window/system32/etct/hosts)  (máy mac là /etc/hosts)  thêm dòng sau 
192.168.56.99 appmvc.net
khi đó trên máy truy cập vào địa chỉ appmvc.net là đang trỏ về server 192.168.56.99
Sau này trong ứng dụng thực tế khi mua tên miền thì tên miền trỏ về địa chỉ server còn cấu hình ở trong apache thì để nguyên 
- Trường hợp thứ 2 là thiết lập lắng nghe ở cổng 443 tương ứng giao thức https thì cần thiết lập chứng chỉ ssl
VirtualHost với chứng chỉ SSL (lắng nghe ở cổng 443)

<VirtualHost *:443>
    DocumentRoot "/home/towebsite/public_html"
    ServerName domain.net
    ServerAlias www.domain.net
    ErrorLog ${APACHE_LOG_DIR}domain.net-error.log
    CustomLog ${APACHE_LOG_DIR}domain.net-access.log common

    # Kích hoạt SSL
    SSLEngine on
    # SSLProtocol all -SSLv2
    # SSLCipherSuite ALL:!ADH:!EXPORT:!SSLv2:!RC4+RSA:+HIGH:+MEDIUM:!LOW:!RC4
    # thiết lập file chứng chỉ SSL (SSLCertificateFile và SSLCertificateKeyFile)
    # Có thế lấy từ dịch vụ Let’s Encrypt
    SSLCertificateFile /certtest/ca.crt #nơi lưu trữ file chứng chỉ
    SSLCertificateKeyFile /certtest/ca.key #key của chứng chỉ 



    # Các chỉ thị khác


</VirtualHost>
- Đối với server thật và tên miền thật có thể lấy chứng chỉ ssl từ dịch vụ miễn phí let encrypt nó sẽ cung cấp file chứng chỉ và publish key của chứng chỉ
- Do mình k có server tên miền thật nên tự phát sinh chứng chỉ ssl sử dụng openssl thực hiện các lệnh sau (xem thêm trong file Triển khai ứng dụng ASP.NET trên Server Linux với Kestrel Apache Nginx)
+ trong thư mục publish trên server tạo thư mục certtest mkdir /certtest
+ Vào thư mục certtest cd /certtest
+ Thực hiện lệnh openssl 
openssl genrsa -out ca.key 2048
openssl req -new -key ca.key -out ca.csr
openssl x509 -req -days 365 -in ca.csr -signkey ca.key -out ca.crt
+ Hoàn thành các lệnh trên sẽ thu được public key tại /certtest/ca.crt và private key tại /certtest/ca.key
+ Khởi động lại Apache, giờ đã có thể truy cập với giao thức https
- Nếu k đc thì ktra lại firewall firewall-cmd --list-services xem có dịch vụ https chưa. Nếu chưa có thì enable https lên bằng lệnh firewall-cmd --zone=public --permanent --add-service=https firewall-cmd --reload
- Thiết lập thêm khi người dùng truy cập http thì tự chuyển hướng sang https thì làm như sau 
bỏ cấu hình proxy trong http thêm vào 
 # chuyển hướng http sang https
    RewriteEngine On
    RewriteCond %{HTTPS} !=on
    RewriteRule ^/?(.*) https://%{SERVER_NAME}/$1 [R,L]
 # triển khai chạy ứng dụng với Nginx
 - Stop apache : systemctl disable httpd systemctl stop httpd
 - Kích hoạt máy chủ ngnix: systemctl enable nginx systemctl start nginx
 - Ktra file cấu hình nginx ở đâu : nginx -t thì sẽ ra file cấu hình nằm ở  /etc/nginx/nginx.conf
 - Tạo 1 server lắng nghe trên cổng 80 (thêm đoạn code sau vào bên trong httpd trong file nginx)
 server {
    listen        80;
    server_name   appmvc.net *.appmvc.net;
    location / {
        proxy_pass         http://localhost:8090;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
khi truy cập cổng 80 thì request đc chuyển đến cổng 8090
 - Khởi động lại nginx: systemctl restart nginx
 - Cấu hình https tạo 1 server lắng nghe ở cổng 443 
 + đầu tiên cấu hình proxy bằng cách tạo file /etc/nginx/proxy.conf có nội dung như sau
 proxy_redirect          off;
proxy_set_header        Host $host;
proxy_set_header        X-Real-IP $remote_addr;
proxy_set_header        X-Forwarded-For $proxy_add_x_forwarded_for;
proxy_set_header        X-Forwarded-Proto $scheme;
client_max_body_size    10m;
client_body_buffer_size 128k;
proxy_connect_timeout   90;
proxy_send_timeout      90;
proxy_read_timeout      90;
proxy_buffers           32 4k;
+ trong file nginx.conf thêm dòng code sau vào phần đầu khối http 
include        /etc/nginx/proxy.conf;
limit_req_zone $binary_remote_addr zone=one:10m rate=5r/s;
server_tokens  off;
client_body_timeout 10; client_header_timeout 10; send_timeout 10;
+ tạo khối upstream là địa chỉ chúng ta sẽ chuyển thông điệp http đến nó
# tạo upstream - nơi nginx chuyển http message đến
upstream mvcblog {
    server localhost:5000;
}
+ Tạo khối server lắng nghe ở cổng 443
server {
    listen                    *:443 ssl;
    server_name               mysite.net;
    ssl_certificate          /đường đến/file/chứng chỉ;
    ssl_certificate_key      /đường đến/file/key;
    ssl_protocols             TLSv1.1 TLSv1.2;
    ssl_prefer_server_ciphers on;
    ssl_ciphers               "EECDH+AESGCM:EDH+AESGCM:AES256+EECDH:AES256+EDH";
    ssl_ecdh_curve            secp384r1;
    ssl_session_cache         shared:SSL:10m;
    ssl_session_tickets       off;
    ssl_stapling              on; #ensure your cert is capable
    ssl_stapling_verify       on; #ensure your cert is capable

    add_header Strict-Transport-Security "max-age=63072000; includeSubdomains; preload";
    add_header X-Frame-Options DENY;
    add_header X-Content-Type-Options nosniff;

    #Redirects all traffic
    location / {
        proxy_pass http://mvcblog;
        limit_req  zone=one burst=10 nodelay;
    }
}
+ Thiết lập header và chuyển hướng https khi người dùng truy cập http sửa code trong khối server lắng nghe ở cổng 80

#chuyển hướng 80 -> 443
server {
    listen     80;
    server_name   mysite.net *.mysite.net;
    add_header Strict-Transport-Security max-age=15768000;
    return     301 https://$host$request_uri;
}
+ Khởi động lại nginx : systemctl restart nginx
+ Ktra lỗi file conf dùng nginx -t hoặc journalctl -xeu nginx.service
