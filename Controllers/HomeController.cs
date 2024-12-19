using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppMvc.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMvc.Net.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public string HiHome() =>  "Xin chao cac ban";
    public IActionResult Index()
    {
        var products = _context.Products
                                .Include(p => p.Author)
                                .Include(p => p.Photos)
                                .Include(p => p.ProductCategoryProducts)
                                .ThenInclude(pc => pc.Category)
                                .Take(4)
                                .AsQueryable();
        products.OrderByDescending(p => p.DateUpdated);
        var posts = _context.Posts
                                .Include(p=>p.Author)
                                .Include(p => p.PostCategories)
                                .ThenInclude(pc => pc.Category)
                                .Take(3)
                                .AsQueryable();
        posts.OrderByDescending(p => p.DateUpdated);
        ViewBag.products = products;
        ViewBag.posts = posts;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
