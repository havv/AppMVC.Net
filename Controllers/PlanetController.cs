using AppMVC.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Net.Controllers
{
    //[Route("he-mat-troi")] //khi đã thiết lập route cho controller thì bắt buộc phải thiết lập cho action. 
    //trong trường hợp ko thiết lập cho action thì phải thêm tham só action vào route trong controller
     [Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger  = logger;
        }
        [BindProperty(SupportsGet =true, Name = "action")]
        public string Name { get; set; }
        // GET: Planet
        //[Route("danh-sach-cac-hanh-tinh.html")]  //he-mat-troi/danh-sach-cac-hanh-tinh.html
        [Route("/danh-sach-cac-hanh-tinh.html")] ///danh-sach-cac-hanh-tinh.html
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult Mercury()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Venus()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Earth()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Mars()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        [HttpGet("/saomoc.html")]
        public IActionResult Jupiter()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Saturn()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Uranus()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        [Route("sao/[action]", Order = 3, Name = "neptune3")] //sao/Neptune
        [Route("sao/[controller]/[action]", Order = 2, Name = "neptune2")] //sao/Planet/Neptune
        [Route("[controller]-[action].html", Order = 1, Name = "neptune1")]  //Planet-Neptune.html
        //Route có thể đc khai báo nhiều lần. Khi gọi thẻ a có action và controller thì chương trình tự chọn ngẫu nhiên 1 trong 3 route để phát sinh url
        //trong trường hợp muốn đặt thứ tự ưu tiên thì dùng tham số Order
        //tham số Name có thể dùng để phát sinh url trong view: @Url.RouteUrl("neptune1")
        public IActionResult Neptune()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        //id là tham số thông thường của Route còn các tham số đặc biệt controller, action, area thì đặt trong dấu ngoặc vuông [controller] [action] [area]
        [Route("hanhtinh/{id:int}")] //hanhtinh/1
        public IActionResult PlanetInfo(int id)
        {
            var planet = _planetService.Where(p=>p.Id == id).FirstOrDefault();
            return View("Detail",planet);

        }

    }
}
