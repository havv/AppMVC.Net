using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMvc.Net.Models;
using Microsoft.AspNetCore.Authorization;
using App.Data;
//using System.Data.Entity;

namespace AppMvc.Net.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/category/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator )]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            //var appDbContext = _context.Category.Include(c => c.ParentCategory);
                        //return View(await appDbContext.ToListAsync());

            var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory)
                        .Include(c => c.CategoryChildren);
            var categories = (await query.ToListAsync())
                            .Where(c => c.ParentCategory == null).ToList();
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        private void CreateSelectItems(List<Category> source, List<Category> des, int level)
        {
            string prefix = string.Concat(Enumerable.Repeat("---", level));
            foreach(var category in source)
            {
                //category.Title = prefix + category.Title;
                des.Add(new Category{
                    Id = category.Id,
                    Title = prefix + " " + category.Title
                });
                if(category.CategoryChildren?.Count > 0)
                {
                    CreateSelectItems(category.CategoryChildren.ToList(), des, level +1);
                }

            }
        }
        // GET: Category/Create
        public async Task<IActionResult> Create()
        {
            var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory);
                        //.Include(c => c.CategoryChildren);
            var categories = (await query.ToListAsync())
                             .Where( c => c.ParentCategory == null)
                             .ToList();
             //var categories = query.ToList();
            categories.Insert(0,new Category{
                Id = -1,
                Title = "Không có danh mục cha"
            });
            //var selectList = new SelectList(categories, "Id", "Title");
            // ViewData["ParentCategoryId"] = selectList;

            var items = new List<Category>();
            CreateSelectItems(categories, items, 0);
             var selectList = new SelectList(items, "Id", "Title");
             ViewData["ParentCategoryId"] = selectList;
            //ViewData["ParentCategoryId"] = new SelectList(_context.Category, "Id", "Title");
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentCategoryId,Title,Description,Slug")] Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.ParentCategoryId == -1 ) category.ParentCategoryId = null;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
              var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory);
                        //.Include(c => c.CategoryChildren);
            var categories = (await query.ToListAsync())
                            .Where( c => c.ParentCategory == null)
                            .ToList();
            categories.Insert(0,new Category{
                Id = -1,
                Title = "Không có danh mục cha"
            });
            //var selectList = new SelectList(categories, "Id", "Title");
            //ViewData["ParentCategoryId"] = selectList;
            //ViewData["ParentCategoryId"] = new SelectList(_context.Category, "Id", "Slug", category.ParentCategoryId);
               var items = new List<Category>();
            CreateSelectItems(categories, items, 0);
             var selectList = new SelectList(items, "Id", "Title");
             ViewData["ParentCategoryId"] = selectList;
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
                 var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory);
                        //.Include(c => c.CategoryChildren);
            var categories = (await query.ToListAsync())
                            .Where( c => c.ParentCategory == null)
                            .ToList();
            categories.Insert(0,new Category{
                Id = -1,
                Title = "Không có danh mục cha"
            });
          
               var items = new List<Category>();
            CreateSelectItems(categories, items, 0);
             var selectList = new SelectList(items, "Id", "Title");
             ViewData["ParentCategoryId"] = selectList;
           // ViewData["ParentCategoryId"] = new SelectList(_context.Category, "Id", "Slug", category.ParentCategoryId);
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentCategoryId,Title,Description,Slug")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if(category.ParentCategoryId == category.Id)
            {
                ModelState.AddModelError(string.Empty, "Phai chon danh muc cha khac");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(category.ParentCategoryId == -1)
                        category.ParentCategoryId = null;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
              var query = (from c in _context.Category select c)
                        .Include(c => c.ParentCategory);
                        //.Include(c => c.CategoryChildren);
            var categories = (await query.ToListAsync())
                            .Where( c => c.ParentCategory == null)
                            .ToList();
            categories.Insert(0,new Category{
                Id = -1,
                Title = "Không có danh mục cha"
            });
           
               var items = new List<Category>();
            CreateSelectItems(categories, items, 0);
             var selectList = new SelectList(items, "Id", "Title");
             ViewData["ParentCategoryId"] = selectList;
            //ViewData["ParentCategoryId"] = new SelectList(_context.Category, "Id", "Slug", category.ParentCategoryId);
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // var category = await _context.Category.FindAsync(id);
            // if (category != null)
            // {
            //     _context.Category.Remove(category);
            // }

            //await _context.SaveChangesAsync();
            var category = await  _context.Category.Include(c => c.CategoryChildren).FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            foreach(var child in category.CategoryChildren)
            {
                child.ParentCategoryId = category.ParentCategoryId;

            }
             _context.Category.Remove(category);
             await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
