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