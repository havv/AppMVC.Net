using AppMvc.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Net.Controllers;

public class FirstController : Controller
{
    private readonly ILogger<FirstController> _logger;
    private readonly ProductService _productService;
    public FirstController(ILogger<FirstController> logger, ProductService productService)
    {
        _logger = logger;
        _productService = productService;

    }
    public string Index()
    {
        // this.HttpContext
        // this.Request
        // this.Response
        // this.RouteData
        // this.User
        // this.ModelState
        // this.ViewData
        // this.ViewBag
        // this.Url
        // this.TempData
        //LogLevel co 6 cap do log 
        _logger.Log(LogLevel.Warning, "Thong bao");
        _logger.LogInformation("Index Action");
        _logger.LogError("Thong bao");
        _logger.LogDebug("Thong bao");
        _logger.LogCritical("Thong bao");
        //Co the su dung log ben thu 3 vd Serilog  thi chi can add service o trong file program ma k can sua code trong controller
        return "Day la ham dau tien";

    }
    public void Nothing()
    {
        _logger.LogInformation("Nothing Action");
        //Response.Headers.Add("hi", "xin chao cac ban");
    }
    //action la phuong thuc public co the tra ve bat ki kieu du lieu nao va ko dc khai bao la static
    public object Anything() => DateTime.Now;
    //Tuy nhien action thuong tra ve cac kieu du lieu trien khai tu lop IActionResult
    /*
    ContentResult Content()
    EmptyResult new EmptyResult()
    FileResult File()
    ForbidResult Forbid()
    JsonResult Json()
    LocalRedirectResult LocaRedirect()
    RedirectResult Redirect()
    RedirectToActionResult RedirectToAction()
    RedirectToPageResult RedirectToPage()
    RedirectToRouteResult RedirectToRoute()
    PartialViewResult PartialView()
    ViewComponentResult ViewComponent()
    StatusCodeResult StatusCode()
    ViewResult View()
    */
        
    public IActionResult Readme()
    {
        var content = @"
        Xin chao cac ban 
        cac ban dang hoc ve ASP.NET MVC



        XUANTHULAB.NET
        ";
        return Content(content,"text/plain");
        //return Content(content,"text/html");

    }

    public IActionResult Bird()
    {
        //var contentRootPath = _env.ContentRootPath;
        string filePath = Path.Combine(Environment.CurrentDirectory, "Files", "bird.jpg");
        var bytes = System.IO.File.ReadAllBytes(filePath);
        return File(bytes, "image/jpg");

    }

    public IActionResult IphonePrice(){
        return Json (
            new {
                productName = "Iphone X",
                price = 1000
            }

        );
    }

    public IActionResult Privacy(){
        var url = Url.Action("Privacy", "Home");
        _logger.LogInformation("Chuyen huong den Privacy");
        return LocalRedirect(url);
    }

    public IActionResult Google()
    {
        var url ="https://www.google.com/";
        _logger.LogInformation("Chuyen huong den google");
        return Redirect(url);
    }

    //ViewResult View()

    public IActionResult HelloView(string username)
    {
        if(string.IsNullOrEmpty(username))
            username = "Khach";
        //View() -> Razor Engine, doc .cshtml
        //view(template)
        //View(template, model)
        //return View("xinchao1", username);
        //return View() /Views/First/HelloView.cshtml  /Views/Controller/Action.cshtml
        //return View((object)username) 
        return View("xinchao3", username);

    }
    [TempData]
    public string StatusMessage { get; set; }
    public IActionResult ViewProduct(int? id)
    {
        var product = _productService.Where( p =>p.Id == id).FirstOrDefault();
        if(product == null)
        {
            //return NotFound();

            //TempData["StatusMessage"] = "San pham ban tim ko co";
            StatusMessage = "San pham ban tim ko co";
            return Redirect(Url.Action("Index", "Home"));
        }
        //return Content($"San pham Id = {id}");
        //Truyen model sang view
        //return View(product);

        //truyen ViewData
        // this.ViewData["product"] = product;
        // ViewData["title"] = product.Name;
        // return View("ViewProduct2");

        //ViewBag
        ViewBag.product = product;
        return View("ViewProduct3");

        //TempData su dung session cua he thong de luu giu lieu nen trang khac co the doc dc du lieu

    }

    

}