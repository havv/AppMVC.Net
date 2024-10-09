using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMvc.Net.Models;
using Microsoft.AspNetCore.Authorization;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Identity;
using AppMvc.Net.Areas.Blog.Models;
using App.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppMvc.Net.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/post/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator+ "," + RoleName.Editor)]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }

        public PostController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Post
        public async Task<IActionResult> Index([FromQuery(Name="p")]int currentPage, int pageSize)
        {
            //var appDbContext = _context.Posts.Include(p => p.Author);
            var posts = _context.Posts.Include(p => p.Author).OrderByDescending(p => p.DateUpdated);
            int totalPosts = await posts.CountAsync();
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
            var postInPage = await posts.Skip((currentPage -1) * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.PostCategories)
                                    .ThenInclude(pc => pc.Category)
                                    .ToListAsync();
            return View(postInPage);

            //  var qr = _userManager.Users.OrderBy(u => u.UserName);

            // model.totalUsers = await qr.CountAsync();
            // model.countPages = (int)Math.Ceiling((double)model.totalUsers / model.ITEMS_PER_PAGE);

            // if (model.currentPage < 1)
            //     model.currentPage = 1;
            // if (model.currentPage > model.countPages)
            //     model.currentPage = model.countPages;

            // var qr1 = qr.Skip((model.currentPage - 1) * model.ITEMS_PER_PAGE)
            //             .Take(model.ITEMS_PER_PAGE)
            //             .Select(u => new UserAndRole() {
            //                 Id = u.Id,
            //                 UserName = u.UserName,
            //             });

            // model.users = await qr1.ToListAsync();

            // foreach (var user in model.users)
            // {
            //     var roles = await _userManager.GetRolesAsync(user);
            //     user.RoleNames = string.Join(",", roles);
            // } 
            
            //return View(await posts.ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        // public IActionResult Create()
        // {
        //     ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
        //     return View();
        // }

        public async Task<IActionResult> CreateAsync()
        {
            var categories  =  await _context.Category.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
            return View();

        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Slug,Content,Published, CategoryIds")] CreatePostModel createPostModel)
        {
           var categories  =  await _context.Category.ToListAsync();
             ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
              if(await _context.Posts.AnyAsync(p => p.Slug == createPostModel.Slug) )
             {
                 ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                 return View(createPostModel);

             }
            //  if(createPostModel.Slug == null)
            //  {
            //     var slug = AppUtilities.GenerateSlug(createPostModel.Title);
            //     ModelState.SetModelValue("Slug", new ValueProviderResult(slug));

            //  }
           
            if (ModelState.IsValid)
            {
                  Post post = new Post 
             {
                Title = createPostModel.Title,
                Description = createPostModel.Description,
                Slug = createPostModel.Slug,
                Published = createPostModel.Published,
                Content = createPostModel.Content,


             };
            
                var user =  _userManager.GetUserAsync(this.User).Result;
                 post.DateCreated = post.DateUpdated = DateTime.Now;
                 post.AuthorId = user.Id.ToString();
                _context.Add(post);

                   if(createPostModel.CategoryIds != null)
                {
                    foreach(var CateId in createPostModel.CategoryIds)
                    {
                        var postCategory = new PostCategory
                        {
                            CategoryId = CateId,
                            Post = post
                        };
                        _context.Add(postCategory);

                    }

                }
              
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            return View(createPostModel);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts.FindAsync(id);
            var post = await _context.Posts.Include(p => p.PostCategories).FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            var postEdit = new CreatePostModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Description = post.Description,
                Slug = post.Slug,
                Published = post.Published,
                CategoryIds = post.PostCategories.Select(pc => pc.CategoryId).ToArray()
            };
            //ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
             var categories  =  await _context.Category.ToListAsync();
             ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
            return View(postEdit);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Description,Slug,Content,Published, CategoryIds")] CreatePostModel post)
        {
            var categories  =  await _context.Category.ToListAsync();
             ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
            if (id != post.PostId)
            {
                return NotFound();
            }
                if(await _context.Posts.AnyAsync(p => p.Slug == post.Slug && p.PostId !=id) )
             {
                 ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                 return View(post);

             }

            if (ModelState.IsValid)
            {
                try
                {
                      var postUpdate = await _context.Posts.Include(p => p.PostCategories).FirstOrDefaultAsync(p => p.PostId == id);
                      if(postUpdate == null)
                      {
                        return NotFound();
                      }
                      postUpdate.Title = post.Title;
                      postUpdate.Description = post.Description;
                      postUpdate.Slug = post.Slug;
                      postUpdate.Content = post.Content;
                      postUpdate.Published = post.Published;
                      var odlCateIds = postUpdate.PostCategories.Select(pc => pc.CategoryId).ToArray();
                      var newCateIds = post.CategoryIds;
                      var removeCatePosts = from postCate in postUpdate.PostCategories
                                            where(!newCateIds.Contains(postCate.CategoryId))
                                            select postCate;
                        _context.PostCategories.RemoveRange(removeCatePosts);
                        var addCateIds = from cateId in newCateIds where !odlCateIds.Contains(cateId) select cateId;
                        foreach(var cateId in addCateIds)
                        {
                            var postCategory = new PostCategory
                            {
                                CategoryId = cateId,
                                //Post = postUpdate
                                PostId = id
                            };
                            _context.PostCategories.Add(postCategory);
                        }
                        
                    //      foreach(var CateId in post.CategoryIds)
                    // {
                    //     var postCategory = new PostCategory
                    //     {
                    //         CategoryId = CateId,
                    //         Post = postUpdate
                    //     };
                    //     _context.Add(postCategory);

                    // }

                    _context.Update(postUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                StatusMessage = "Vua cap nhat bai viet";
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            StatusMessage = "Bạn vừa xóa bài viết " + post.Title;
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
