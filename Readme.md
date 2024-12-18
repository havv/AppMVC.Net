## Controller
- La 1 lop ke thua tu lop controller : Microsoft.AspNetCore.Mvc
- Action trong controller la mot phuong thuc public (ko dc static)
- Action tra ve bat ky kieu du lieu nao, thuong la IActionResult
- Cac dich vu inject vao controller qua ham tao

##View
- La file .cshtml
- view cho action luu tai /Views/ControllerName/ActionName.cshmtl
- them thu muc luu tru view (them vao file Program)
 //{0} -> ten Action 
   //{1} -> ten Controller
   //{2} -> ten Area
   //RazorViewEngine.ViewExtension ~ cshtml
   options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

##Truyen du lieu sang View
-Model
-ViewData
-ViewBag
-TempData

#Day code len github
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

#Cài đặt tool generator
 + dotnet tool install -g dotnet-aspnet-codegenerator (cài đặt công cụ codegenerator)
 + dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design (add package)
 + dotnet aspnet-codegenerator -h (Gõ lệnh để xem hướng dẫn)
 + dotnet aspnet-codegenerator controller -name ProductController -namespace AppMvc.Net.Controllers -outDir Controllers (tạo controller)

#Area để thiết lập controller thuộc về 1 vùng nào đó
- tên area được dùng để thiết lập routing
- Là cấu trúc thư mục chứa MVC (dễ quản lý hơn)
- Thiết lập area cho controller  [Area("AreaName")]
- Tạo cấu trúc thư mục dotnet asp-codegenerator area Product

#Phát sinh các url (xem ví dụ trong file index của home)
-Url.ActionLink() Url.Action() sử dụng action để sinh ra url
- Url.RouteUrl() Url.Link() sử dụng tên route để sinh ra url
- <a> <form> <button>

#Tích hợp Entity Framework và các công cụ phát sinh code
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package MySql.Data.EntityFramework

#Docker với sql
- Chạy SQL Server trên docker chạy lệnh ở thư mục chứa docker-compose.yml
- chạy dịch vụ docker-compose up -d
- khởi động nếu dịch vụ bị dừng docker-compose start
- xóa dịch vụ docker-compose down 
- ktra container đang chạy docker ps

#Dùng migration để tạo cơ sở dữ liệu
- dotnet ef migrations add Init (tạo migration đầu tiên)
- dotnet ef database update
- Xem thêm code trong thư mục Areas/Database

#Thiết lập cấu hình log information khi chạy ứng dụng sẽ xuất hiện thông tin trên màn hình terminal
- Thêm các thông tin muốn log vào file appsetting.json 
 + ví dụ "Microsof.EntityFrameworkCore.Query" : "Information"
         "Microsof.EntityFrameworkCore.Database.Command" : "Information"
   tham khảo thêm xem thêm thông tin simple logging entity framework Message categories