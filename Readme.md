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
