using AppMvc.Net.Services;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddTransient(typeof(ILogger<>),typeof(Logger<>)); mac dinh logger da dc add nen k can lenh nay neu muon su dung dich vu log cua ben thu 3 thi chi can sua phuong thuc nay vd Serilog ma k can sua o trong controller
//builder.Services.AddSingleton<ProductService>();
//builder.Services.AddSingleton<ProductService, ProductService>();
//builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));


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

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// app.UseEndpoints(endpoints => {
//     endpoints.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//     endpoints.MapRazorPages();

// });


app.Run();
