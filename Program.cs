using System.Configuration;
using System.Net;
using App.Data;
using App.Services;
using AppMvc.Net.ExtendMethods;
using AppMvc.Net.Models;
using AppMvc.Net.Services;
using AppMVC.Net.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectString = builder.Configuration.GetConnectionString("AppMvcConnectionString");
    options.UseSqlServer(connectString);
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddTransient(typeof(ILogger<>),typeof(Logger<>)); mac dinh logger da dc add nen k can lenh nay neu muon su dung dich vu log cua ben thu 3 thi chi can sua phuong thuc nay vd Serilog ma k can sua o trong controller
//builder.Services.AddSingleton<ProductService>();
//builder.Services.AddSingleton<ProductService, ProductService>();
//builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));
builder.Services.AddSingleton<PlanetService>();

// Dang ky Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất


    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/khongduoctruycap.html";
});

builder.Services.AddAuthentication()
       .AddGoogle(options =>
       {
           var gconfig = builder.Configuration.GetSection("Authentication:Google");
           options.ClientId = gconfig["ClientId"];
           options.ClientSecret = gconfig["ClientSecret"];
           // https://localhost:5001/signin-google
           options.CallbackPath = "/dang-nhap-tu-google";
       })
       .AddFacebook(options =>
       {
           var fconfig = builder.Configuration.GetSection("Authentication:Facebook");
           options.AppId = fconfig["AppId"];
           options.AppSecret = fconfig["AppSecret"];
           options.CallbackPath = "/dang-nhap-tu-facebook";
       })
       // .AddTwitter()
       // .AddMicrosoftAccount()
       ;
builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
//Thiet lap policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(RoleName.Administrator);
    });

});

//Thiet lap cau hinh cho razor engine 
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
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
//Thiết lập truy cập file tĩnh lưu trong thư mục wwwroot
app.UseStaticFiles();

//Thiết lập truy cập file tĩnh lưu trong thư mục Uploads 
app.UseStaticFiles(new StaticFileOptions(){
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/contents" //  /contents/1.jpg => Uploads/1.jpg

});
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
app.UseAuthentication(); //xac dinh danh tinh
app.UseAuthorization(); //xac thuc quyen truy cap
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
app.MapGet("/sayhi", async context =>
{
    await context.Response.WriteAsync("Xin chao day la say hi");
});
//Cac phương thức ánh xạ url vào controller
// app.MapControllers
// app.MapControllerRoute
// app.MapDefaultControllerRoute
// app.MapAreaControllerRoute
app.MapControllerRoute(
    name: "first",
    //pattern:"xemsanpham/{id?}", //xemsanpham/1
    pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(2,4)}", //abcanything/1
    defaults: new
    {
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
    name: "product",
    pattern: "/{controller}/{action=Index}/{id?}",
    areaName: "ProductManage"
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
