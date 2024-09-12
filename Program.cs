using System.Net;
using AppMvc.Net.ExtendMethods;
using AppMvc.Net.Services;
using AppMVC.Net.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddTransient(typeof(ILogger<>),typeof(Logger<>)); mac dinh logger da dc add nen k can lenh nay neu muon su dung dich vu log cua ben thu 3 thi chi can sua phuong thuc nay vd Serilog ma k can sua o trong controller
//builder.Services.AddSingleton<ProductService>();
//builder.Services.AddSingleton<ProductService, ProductService>();
//builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));
builder.Services.AddSingleton<PlanetService>();

//Thiet lap cau hinh cho razor engine 
builder.Services.Configure<RazorViewEngineOptions>(options =>{
   // Mac dinh se tim view o /Views/Controller/Action.cshtml
   //Thiet lap them  /MyView/Controller/Action.cshtml
   //{0} -> ten Action 
   //{1} -> ten Controller
   //{2} -> ten Area
   //RazorViewEngine.ViewExtension ~ cshtml
   options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseStatusCodePages(); // la 1 middleware tao response tu loi 400  - 599
app.AddStatusCodePage(); //Tùy biến cho response từ lỗi 400 -599 
//Tùy biến thêm nội dung trả về của UseStatusCodePages
//  app.UseStatusCodePages(appError => {
//     appError.Run(async context => {
//         var response = context.Response;
//         var code = response.StatusCode;
//       //var content = "loi";
//         var content = @$"<html>
//         <head>
//             <meta charset='UTF-8'/>
//             <title></title>
//         </head>
//         <body>
//             <p style='color:red; font-size: 30px'>
//                 Co loi xay ra: {code} - {(HttpStatusCode)code}
//             </p>
//         </body>
//         </html";
//         await response.WriteAsync(content);
//     });
// });
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

//tao endpoint theo cach viet cu
// app.UseEndpoints(endpoints => { 
//     endpoints.MapGet("/sayhi", async context => {
//      await  context.Response.WriteAsync("Xin chao day la say hi");
//     });
//     endpoints.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//     endpoints.MapRazorPages();
// });

//Tao endpoint
app.MapGet("/sayhi", async context => {
   await  context.Response.WriteAsync("Xin chao day la say hi");
});
//Cac phương thức ánh xạ url vào controller
// app.MapControllers
// app.MapControllerRoute
// app.MapDefaultControllerRoute
// app.MapAreaControllerRoute
app.MapControllerRoute(
    name: "first",
    //pattern:"xemsanpham/{id?}", //xemsanpham/1
    pattern:"{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(2,4)}", //abcanything/1
    defaults : new {
        controller = "First",
        action = "ViewProduct"

    }
    //thiết lập ràng buộc của tham số
    // constraints: new {
    //     //url = new StringRouteConstraint("xemsanpham")
    //     //url = "xemsanpham",
    //     url = new RegexRouteConstraint(@"^((xemsanpham)|(viewproduct))$"),
    //     id =  new RangeRouteConstraint(2,4)


    // }
    //IRouteConstraint
    //new StringRouteConstraint("xemsanpham")
);
// app.MapControllerRoute(
//     name: "default",
//     pattern: "start-here/{controller=Home}/{action=Index}/{id?}"

//     //pattern: "start-here/{controller}/{action}/{id?}"

//     // pattern: "start-here/{id}",
//     // defaults: new {
//     //     controller = "First",
//     //     action = "ViewProduct",
//     //     id = 3
//     // }
// );
app.MapAreaControllerRoute(
    name:"product",
    pattern: "/{controller}/{action=Index}/{id?}",
    areaName:"ProductManage"
);
app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=Home}/{action=Index}/{id?}"

    //pattern: "start-here/{controller}/{action}/{id?}"

    // pattern: "start-here/{id}",
    // defaults: new {
    //     controller = "First",
    //     action = "ViewProduct",
    //     id = 3
    // }
);

//Thiết lập các attribute cho các controller 
//[AcceptVerbs]
//[Route]
// [HttpGet]
// [HttpPost]
// [HttpPut]
// [HttpDelete]
// [HttpPatch]
// [HttpHead]
app.Run();
