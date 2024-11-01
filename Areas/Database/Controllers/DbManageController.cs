using AppMvc.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMvc.Net.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public DbManageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        // GET: DbManageController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }
        [TempData]
        public string StatusMessage {get;set;}
        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _appDbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xóa thành công" : "Không xóa được";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Migrate()
        {
            await _appDbContext.Database.MigrateAsync();
            StatusMessage = "Cập nhật db thành công";
            return RedirectToAction(nameof(Index));
        }

    }
}
