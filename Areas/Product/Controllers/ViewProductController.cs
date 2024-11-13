//using System.Data.Entity;
using App.Models;
using AppMvc.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AppMvc.Net.Areas.Product.Controllers
{
    [Area("Product")]
    public class ViewProductController : Controller
    {
        private readonly ILogger<ViewProductController> _logger;
        private AppDbContext _context;
        private readonly CartService _cartService;
        public ViewProductController(ILogger<ViewProductController> logger, AppDbContext context, CartService cartService)
        {
            _logger = logger;
            _context = context;
            _cartService = cartService;

        }
        // GET: ViewPostController
        [Route("/product/{categoryslug?}")]
        public ActionResult Index(string categoryslug, [FromQuery(Name = "p")] int currentPage, int pageSize)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;
            ViewBag.categoryslug = categoryslug;
            CategoryProduct category = null;
            if (!string.IsNullOrEmpty(categoryslug))
            {
                category = _context.CategoryProducts.Where(c => c.Slug == categoryslug)
                                            .Include(c => c.CategoryChildren)
                                            .FirstOrDefault();
                if (category == null)
                {
                    return NotFound("Khong thay category");
                }
            }
            var products = _context.Products
                                .Include(p => p.Author)
                                .Include(p => p.Photos)
                                .Include(p => p.ProductCategoryProducts)
                                .ThenInclude(pc => pc.Category)
                                .AsQueryable();
            products.OrderByDescending(p => p.DateUpdated);
            //Dung cach duoi cung dc nhung neu ko co AsQueryable thi dong code 53  posts = posts.Where(p =>p.PostCategories.Where(pc => ids.Contains(pc.CategoryId)).Any()); bi loi check lai sau
            //var posts = _context.Posts
            //.Include(p=>p.Author).Include(p => p.PostCategories).ThenInclude(pc => pc.Category).OrderByDescending(p => p.DateUpdated);
            //var posts = (from c in _context.Posts select c).Include(p=>p.Author).Include(p => p.PostCategories).OrderByDescending(p => p.DateUpdated); //.ThenInclude(pc => pc.Category);
            if (category != null)
            {
                var ids = new List<int>();
                category.ChildCategoryIds(ids, null);
                ids.Add(category.Id);
                products = products.Where(p => p.ProductCategoryProducts.Where(pc => ids.Contains(pc.CategoryId)).Any());
            }
            int totalProducts = products.Count();
            if (pageSize <= 0) pageSize = 10;
            int countPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;
            var pagingModel = new PagingModel
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pageSize = pageSize
                })

            };
            ViewBag.postIndex = (currentPage - 1) * pageSize;
            ViewBag.PagingModel = pagingModel;
            ViewBag.totalProducts = totalProducts;
            var productInPage = products.Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize);

            ViewBag.category = category;
            //var postmodel = posts.ToList();
            //return View(postmodel);
            return View(productInPage.ToList());

        }

        [Route("/product/{productslug}.html")]
        public ActionResult Detail(string productslug)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;
            var product = _context.Products.Where(p => p.Slug == productslug)
                                     .Include(p => p.Author)
                                     .Include(p => p.Photos)
                                     .Include(p => p.ProductCategoryProducts)
                                     .ThenInclude(pc => pc.Category)
                                     .FirstOrDefault();
            if (product == null)
                return NotFound("Khong tim thay bai viet");
            CategoryProduct category = product.ProductCategoryProducts.FirstOrDefault()?.Category;
            ViewBag.category = category;
            var otherProducts = _context.Products.Where(p => p.ProductCategoryProducts.Any(c => c.Category.Id == category.Id) && p.ProductId != product.ProductId)
                                           .OrderByDescending(p => p.DateUpdated)
                                           .Take(5);
            ViewBag.otherProducts = otherProducts;
            return View(product);
        }

        private List<CategoryProduct> GetCategories()
        {
            var categories = _context.CategoryProducts
                                     .Include(c => c.CategoryChildren)
                                     .AsEnumerable() //nếu bỏ AsEnumerable thì data ko có category con ?? chưa hiểu tìm đọc lại sau. 
                                     .Where(c => c.ParentCategory == null)
                                     .ToList();
            return categories;
        }
        /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.Products
                .Where(p => p.ProductId == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            _cartService.SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }
        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(_cartService.GetCartItems());
        }
        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            _cartService.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            _cartService.SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        [Route("/checkout")]
        public IActionResult Checkout()
        {
           var cart = _cartService.GetCartItems();
           /*Xây dựng trang yêu cầu người dùng nhập thông tin sđt, địa chỉ gửi hàng ..
            Sau đó lấy thông tin cart gửi email cho khách hàng (xem phần contact)
            Lưu trữ thông tin vào cơ sở dữ liệu*/
            _cartService.ClearCart();
            return Content("Đã gửi đơn hàng");


        }

    }
}
