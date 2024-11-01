using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMvc.Net.Models;
using Microsoft.AspNetCore.Authorization;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Identity;
using AppMvc.Net.Areas.Blog.Models;
using AppMvc.Net.Areas.Product.Models;
using System.ComponentModel.DataAnnotations;

namespace AppMvc.Net.Areas.Product.Controllers
{
    [Area("Product")]
    [Route("admin/product/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator+ "," + RoleName.Editor)]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }

        public ProductController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Post
        public async Task<IActionResult> Index([FromQuery(Name="p")]int currentPage, int pageSize)
        {
            //var appDbContext = _context.Posts.Include(p => p.Author);
            var products = _context.Products.Include(p => p.Author).OrderByDescending(p => p.DateUpdated);
            int totalPosts = await products.CountAsync();
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
            var productInPage = await products.Skip((currentPage -1) * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.ProductCategoryProducts)
                                    .ThenInclude(pc => pc.Category)
                                    .ToListAsync();
            return View(productInPage);

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

            var product = await _context.Products
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Post/Create
        // public IActionResult Create()
        // {
        //     ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
        //     return View();
        // }

        public async Task<IActionResult> CreateAsync()
        {
            var categories  =  await _context.CategoryProducts.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
            return View();

        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Slug,Content,Published, CategoryIds, Price")] CreateProductModel createProductModel)
        {
           var categories  =  await _context.Category.ToListAsync();
             ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
              if(await _context.Posts.AnyAsync(p => p.Slug == createProductModel.Slug) )
             {
                 ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                 return View(createProductModel);

             }
            //  if(createPostModel.Slug == null)
            //  {
            //     var slug = AppUtilities.GenerateSlug(createPostModel.Title);
            //     ModelState.SetModelValue("Slug", new ValueProviderResult(slug));

            //  }
           
            if (ModelState.IsValid)
            {
                  AppMvc.Net.Models.Product product = new AppMvc.Net.Models.Product 
             {
                Title = createProductModel.Title,
                Description = createProductModel.Description,
                Slug = createProductModel.Slug,
                Published = createProductModel.Published,
                Content = createProductModel.Content,
                Price = createProductModel.Price


             };
            
                var user =  _userManager.GetUserAsync(this.User).Result;
                 product.DateCreated = product.DateUpdated = DateTime.Now;
                 product.AuthorId = user.Id.ToString();
                _context.Add(product);

                   if(createProductModel.CategoryIds != null)
                {
                    foreach(var CateId in createProductModel.CategoryIds)
                    {
                        var productCategoryProduct = new ProductCategoryProduct
                        {
                            CategoryId = CateId,
                            Product = product
                        };
                        _context.Add(productCategoryProduct);

                    }

                }
              
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            return View(createProductModel);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts.FindAsync(id);
            var product = await _context.Products.Include(p => p.ProductCategoryProducts).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var productEdit = new CreateProductModel
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Content = product.Content,
                Description = product.Description,
                Slug = product.Slug,
                Published = product.Published,
                CategoryIds = product.ProductCategoryProducts.Select(pc => pc.CategoryId).ToArray(),
                Price = product.Price
            };
            //ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
             var categories  =  await _context.CategoryProducts.ToListAsync();
             ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
            return View(productEdit);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Description,Slug,Content,Published, CategoryIds, Price")] CreateProductModel product)
        {
            var categories  =  await _context.Category.ToListAsync();
             ViewData["categories"] = new MultiSelectList(categories, "Id", "Title");
            if (id != product.ProductId)
            {
                return NotFound();
            }
                if(await _context.Products.AnyAsync(p => p.Slug == product.Slug && p.ProductId !=id) )
             {
                 ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                 return View(product);

             }

            if (ModelState.IsValid)
            {
                try
                {
                      var productUpdate = await _context.Products.Include(p => p.ProductCategoryProducts).FirstOrDefaultAsync(p => p.ProductId == id);
                      if(productUpdate == null)
                      {
                        return NotFound();
                      }
                      productUpdate.Title = product.Title;
                      productUpdate.Description = product.Description;
                      productUpdate.Slug = product.Slug;
                      productUpdate.Content = product.Content;
                      productUpdate.Published = product.Published;
                      productUpdate.Price = product.Price;
                      var odlCateIds = productUpdate.ProductCategoryProducts.Select(pc => pc.CategoryId).ToArray();
                      var newCateIds = product.CategoryIds;
                      var removeCateProducts = from productCate in productUpdate.ProductCategoryProducts
                                            where(!newCateIds.Contains(productCate.CategoryId))
                                            select productCate;
                        _context.ProductCategoryProducts.RemoveRange(removeCateProducts);
                        var addCateIds = from cateId in newCateIds where !odlCateIds.Contains(cateId) select cateId;
                        foreach(var cateId in addCateIds)
                        {
                            var productCategoryProduct = new ProductCategoryProduct
                            {
                                CategoryId = cateId,
                                //Post = postUpdate
                                ProductId = id
                            };
                            _context.ProductCategoryProducts.Add(productCategoryProduct);
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

                    _context.Update(productUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            StatusMessage = "Bạn vừa xóa bài viết " + product.Title;
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        public class UploadOneFile
        {
            [Required(ErrorMessage = "Phải chọn file upload")]
            [DataType(DataType.Upload)]
            [FileExtensions(Extensions = "png, jpg, jpeg, gif")]
            [Display(Name = "Chọn file upload")]
            public IFormFile FileUpload { get; set; }
        }
        [HttpGet]
        public IActionResult UploadPhoto(int id)
        {
            var product = _context.Products.Where(p => p.ProductId == id).Include(p => p.Photos).FirstOrDefault();
            if(product == null)
              return NotFound();
            ViewData["product"] = product;
            return View(new UploadOneFile());

        }
         [HttpPost, ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(int id,[Bind("FileUpload")] UploadOneFile f)
        {
            var product = _context.Products.Where(p => p.ProductId == id).Include(p => p.Photos).FirstOrDefault();
            if(product == null)
              return NotFound();
            ViewData["product"] = product;
            if(f!= null)
            {
                //Phát sinh tên file
                var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                            + Path.GetExtension(f.FileUpload.FileName);
                var file = Path.Combine("Uploads", "Products", file1);

                //Tạo filestream để copy dữ liệu FileUpload
                using(var filestream = new FileStream(file,FileMode.Create))
                {
                    await f.FileUpload.CopyToAsync(filestream);
                }
                //Thêm photo vào database
                _context.Add(new ProductPhoto(){
                    ProductId = product.ProductId,
                    FileName = file1

                });
                await _context.SaveChangesAsync();

            }
            return View(f);

        }
        [HttpPost]
        public IActionResult ListPhotos(int id)
        {
            var product = _context.Products.Where(p => p.ProductId == id).Include(p => p.Photos).FirstOrDefault();
            if(product == null)
            {
                return Json(
                    new {
                        success = 0,
                        message = "Product not found"
                    }
                );
            }
            var listPhotos = product.Photos.Select(photo => new {
                id = photo.Id,
                path = "/contents/Products/" + photo.FileName
            });
            return Json (new {
                success = 1,
                photos = listPhotos
            });
        }

        [HttpPost]
        public IActionResult DeletePhoto(int id)
        {
            var photo = _context.ProductPhotos.Where(p => p.Id == id).FirstOrDefault();
            if(photo == null)
            {
                return Json(
                    new {
                        success = 0,
                        message = "Product not found"
                    }
                );
            }
            _context.ProductPhotos.Remove(photo);
            _context.SaveChanges();
            var fileName = "Uploads/Products/" + photo.FileName;
            System.IO.File.Delete(fileName);
             return Ok();
        }
        [HttpPost]
          public async Task<IActionResult> UploadPhotoApi(int id,[Bind("FileUpload")] UploadOneFile f)
        {
            var product = _context.Products.Where(p => p.ProductId == id).Include(p => p.Photos).FirstOrDefault();
            if(product == null)
              return NotFound();
            ViewData["product"] = product;
            if(f!= null)
            {
                //Phát sinh tên file
                var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                            + Path.GetExtension(f.FileUpload.FileName);
                var file = Path.Combine("Uploads", "Products", file1);

                //Tạo filestream để copy dữ liệu FileUpload
                using(var filestream = new FileStream(file,FileMode.Create))
                {
                    await f.FileUpload.CopyToAsync(filestream);
                }
                //Thêm photo vào database
                _context.Add(new ProductPhoto(){
                    ProductId = product.ProductId,
                    FileName = file1

                });
                await _context.SaveChangesAsync();

            }
            return Ok();

        }
    }
}
