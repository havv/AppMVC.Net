//using System.Data.Entity;
using App.Models;
using AppMvc.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AppMvc.Net.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class ViewPostController : Controller
    {
        private readonly ILogger<ViewPostController> _logger;
        private AppDbContext _context;
        public ViewPostController(ILogger<ViewPostController> logger, AppDbContext context)
        {
            _logger = logger ;
            _context = context;
            
        }
        // GET: ViewPostController
        [Route("/post/{categoryslug?}")]
        public ActionResult Index(string categoryslug,[FromQuery(Name = "p")]int currentPage, int pageSize)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;
            ViewBag.categoryslug = categoryslug;
            Category category = null;
            if(!string.IsNullOrEmpty(categoryslug))
            {
                category = _context.Category.Where(c => c.Slug == categoryslug)
                                            .Include(c => c.CategoryChildren)
                                            .FirstOrDefault();
                if(category == null)
                {
                    return NotFound("Khong thay category");
                }
            }
            var posts = _context.Posts
                                .Include(p=>p.Author)
                                .Include(p => p.PostCategories)
                                .ThenInclude(pc => pc.Category)
                                .AsQueryable();
            posts.OrderByDescending(p => p.DateUpdated);
            //Dung cach duoi cung dc nhung neu ko co AsQueryable thi dong code 53  posts = posts.Where(p =>p.PostCategories.Where(pc => ids.Contains(pc.CategoryId)).Any()); bi loi check lai sau
            //var posts = _context.Posts
                                //.Include(p=>p.Author).Include(p => p.PostCategories).ThenInclude(pc => pc.Category).OrderByDescending(p => p.DateUpdated);
            //var posts = (from c in _context.Posts select c).Include(p=>p.Author).Include(p => p.PostCategories).OrderByDescending(p => p.DateUpdated); //.ThenInclude(pc => pc.Category);
           if(category != null)
           {
            var ids = new List<int>();
            category.ChildCategoryIds(ids, null);
            ids.Add(category.Id);
            posts = posts.Where(p =>p.PostCategories.Where(pc => ids.Contains(pc.CategoryId)).Any());
           }
           int totalPosts =  posts.Count();
            if (pageSize <= 0 ) pageSize = 10;
            int countPages = (int) Math.Ceiling((double)totalPosts/pageSize);
            if(currentPage > countPages) currentPage = countPages;
            if(currentPage < 1) currentPage = 1;
            var pagingModel = new PagingModel
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new {
                    p = pageNumber,
                    pageSize = pageSize
                })

            };
            ViewBag.postIndex = (currentPage - 1) * pageSize;
            ViewBag.PagingModel = pagingModel;
            ViewBag.totalPosts = totalPosts;
            var postInPage =  posts.Skip((currentPage -1) * pageSize)
                                    .Take(pageSize);
                                  
            ViewBag.category = category;
            //var postmodel = posts.ToList();
            //return View(postmodel);
            return View(postInPage.ToList());

        }

        [Route("/post/{postslug}.html")]
        public ActionResult Detail(string postslug)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;
            var post = _context.Posts.Where(p => p.Slug == postslug)
                                     .Include(p => p.Author)
                                     .Include(p => p.PostCategories)
                                     .ThenInclude(pc => pc.Category)
                                     .FirstOrDefault();
            if(post == null)
                return NotFound("Khong tim thay bai viet");
             Category category = post.PostCategories.FirstOrDefault()?.Category;
             ViewBag.category = category;
             var otherPosts = _context.Posts.Where(p => p.PostCategories.Any(c => c.Category.Id == category.Id) && p.PostId != post.PostId)
                                            .OrderByDescending(p => p.DateUpdated)
                                            .Take(5);
            ViewBag.otherPosts = otherPosts;
            return View(post);
        }

        private List<Category> GetCategories()
        {
            var categories = _context.Category
                                     .Include(c => c.CategoryChildren)
                                     .AsEnumerable() //nếu bỏ AsEnumerable thì data ko có category con ?? chưa hiểu tìm đọc lại sau. 
                                     .Where(c => c.ParentCategory == null)
                                     .ToList();
            return categories;
        }

    }
}
