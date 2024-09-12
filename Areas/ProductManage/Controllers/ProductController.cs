using AppMvc.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Net.Controllers
{
    [Area("ProductManage")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
            
        }
        // GET: ProductController
        [Route("/cac-san-pham/{id?}")]
        public ActionResult Index()
        {
            //Areas/AreaName/Views/ControllerName/Action.cshtml
            var product = _productService.OrderBy(p => p.Name).ToList();
            return View(product); //Areas/ProductManage/Views/Product/Index.cshtml
        }

    }
}
