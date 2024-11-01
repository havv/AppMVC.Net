using App.Data;
using AppMvc.Net.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMvc.Net.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbManageController(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;

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
        public string StatusMessage { get; set; }
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
        public async Task<IActionResult> SeedDataAsync()
        {
            //Create Roles
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolenames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename));
                }
            }
            //create user admin, pass = admin123, admin@example.com
            //var useradmin = await _userManager.FindByEmailAsync("admin");
             //var useradmin = await _userManager.FindByEmailAsync("admin@example.com");
            var useradmin = await _userManager.FindByNameAsync("admin");


            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
            }
            SeedPostCategory();
            SeedProductCategory();
            StatusMessage = "Vua seed database";
            return RedirectToAction("Index");


        }
        private void SeedPostCategory()
        {
            //category
            _appDbContext.Category.RemoveRange(_appDbContext.Category.Where(c => c.Description.Contains("[fakeData]")));
            _appDbContext.Posts.RemoveRange(_appDbContext.Posts.Where(c => c.Description.Contains("[fakeData]")));
            _appDbContext.SaveChanges();


            var fakerCategory = new Faker<Category>();
            int cm = 1;
            fakerCategory.RuleFor(c => c.Title, fk => $"CM{cm++}" + fk.Lorem.Sentence(1, 2).Trim('.'));
            fakerCategory.RuleFor(c => c.Description, fk => fk.Lorem.Sentence(5) + "[fakeData]");
            fakerCategory.RuleFor(c => c.Slug, fk => fk.Lorem.Slug());

            var cate1 = fakerCategory.Generate();
            var cate11 = fakerCategory.Generate();
            var cate12 = fakerCategory.Generate();
            var cate2 = fakerCategory.Generate();
            var cate21 = fakerCategory.Generate();
            var cate211 = fakerCategory.Generate();
            cate11.ParentCategory = cate1;
            cate12.ParentCategory = cate1;
            cate21.ParentCategory = cate2;

            cate211.ParentCategory = cate21;
            var categories = new Category[]{cate1, cate2, cate12, cate11, cate21, cate211};
            _appDbContext.Category.AddRange(categories);
            //end category

            //Post
              _appDbContext.Posts.RemoveRange(_appDbContext.Posts.Where(p => p.Description.Contains("[fakeData]")));

            var rCateIndex = new Random();
            int bv = 1;
            var user = _userManager.GetUserAsync(this.User).Result;
            var fackerPost = new Faker<Post>();
            fackerPost.RuleFor(p => p.AuthorId, f => user.Id);
            fackerPost.RuleFor(p => p.Content, f => f.Lorem.Paragraphs(7)+"[fakerData]");
            fackerPost.RuleFor(p => p.DateCreated, f => f.Date.Between(new DateTime(2021,1,1), new DateTime(2021, 7,1)));
            fackerPost.RuleFor(p => p.Description, f => f.Lorem.Sentences(3));
            fackerPost.RuleFor(p => p.Published, fackerPost => true);
            fackerPost.RuleFor(p => p.Slug, f => f.Lorem.Slug());
            fackerPost.RuleFor(p => p.Title, f => $"Bai {bv++} " + f.Lorem.Sentence(3,4).Trim('.') );
            List<Post> posts = new List<Post>();
            List<PostCategory> postCategories = new List<PostCategory>();
            for(int i = 0; i < 40; i++)
            {
                var post = fackerPost.Generate();
                post.DateUpdated = post.DateCreated;
                posts.Add(post);
                postCategories.Add(new PostCategory(){
                    Post = post,
                    Category = categories[rCateIndex.Next(5)]

                });
            }
            _appDbContext.Posts.AddRange(posts);
            _appDbContext.PostCategories.AddRange(postCategories);
            //End Post
            _appDbContext.SaveChanges();



        }
         private void SeedProductCategory()
        {
            //category
            _appDbContext.CategoryProducts.RemoveRange(_appDbContext.CategoryProducts.Where(c => c.Description.Contains("[fakeData]")));
            _appDbContext.Products.RemoveRange(_appDbContext.Products.Where(c => c.Description.Contains("[fakeData]")));
            _appDbContext.SaveChanges();

            var fakerCategory = new Faker<CategoryProduct>();
            int cm = 1;
            fakerCategory.RuleFor(c => c.Title, fk => $"Nhom SP{cm++}" + fk.Lorem.Sentence(1, 2).Trim('.'));
            fakerCategory.RuleFor(c => c.Description, fk => fk.Lorem.Sentence(5) + "[fakeData]");
            fakerCategory.RuleFor(c => c.Slug, fk => fk.Lorem.Slug());

            var cate1 = fakerCategory.Generate();
            var cate11 = fakerCategory.Generate();
            var cate12 = fakerCategory.Generate();
            var cate2 = fakerCategory.Generate();
            var cate21 = fakerCategory.Generate();
            var cate211 = fakerCategory.Generate();
            cate11.ParentCategory = cate1;
            cate12.ParentCategory = cate1;
            cate21.ParentCategory = cate2;

            cate211.ParentCategory = cate21;
            var categories = new CategoryProduct[]{cate1, cate2, cate12, cate11, cate21, cate211};
            _appDbContext.CategoryProducts.AddRange(categories);
            //end category

            //Post
              _appDbContext.Products.RemoveRange(_appDbContext.Products.Where(p => p.Description.Contains("[fakeData]")));

            var rCateIndex = new Random();
            int bv = 1;
            var user = _userManager.GetUserAsync(this.User).Result;
            var fackerProduct = new Faker<Product>();
            fackerProduct.RuleFor(p => p.AuthorId, f => user.Id);
            fackerProduct.RuleFor(p => p.Content, f => f.Commerce.ProductDescription()+"[fakerData]");
            fackerProduct.RuleFor(p => p.DateCreated, f => f.Date.Between(new DateTime(2021,1,1), new DateTime(2021, 7,1)));
            fackerProduct.RuleFor(p => p.Description, f => f.Lorem.Sentences(3));
            fackerProduct.RuleFor(p => p.Published, fackerPost => true);
            fackerProduct.RuleFor(p => p.Slug, f => f.Lorem.Slug());
            fackerProduct.RuleFor(p => p.Title, f => $"Bai {bv++} " + f.Lorem.Sentence(3,4).Trim('.') );
             fackerProduct.RuleFor(p => p.Price, f => int.Parse(f.Commerce.Price(500,100,0)) );

            List<Product> products = new List<Product>();
            List<ProductCategoryProduct> productCategories = new List<ProductCategoryProduct>();
            for(int i = 0; i < 40; i++)
            {
                var product = fackerProduct.Generate();
                product.DateUpdated = product.DateCreated;
                products.Add(product);
                productCategories.Add(new ProductCategoryProduct(){
                    Product = product,
                    Category = categories[rCateIndex.Next(5)]

                });
            }
            _appDbContext.Products.AddRange(products);
            _appDbContext.ProductCategoryProducts.AddRange(productCategories);
            //End Post
            _appDbContext.SaveChanges();



        }
        

    }
}
