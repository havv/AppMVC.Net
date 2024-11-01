using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppMvc.Net.Models;

namespace AppMvc.Net.Models
{
    // razorweb.models.MyBlogContext
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Slug).IsUnique();
            });
            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(c => new { c.PostId, c.CategoryId });

            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(p => p.Slug).IsUnique();
            });
            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasIndex(c => c.Slug).IsUnique();
            });
            modelBuilder.Entity<ProductCategoryProduct>(entity =>
            {
                entity.HasKey(c => new { c.ProductId, c.CategoryId });

            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Slug).IsUnique();
            });

        }

        public DbSet<ContactModel> Contacts { get; set; }
        // razorweb.models.MyBlogContext
        public DbSet<Category> Category { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategoryProduct> ProductCategoryProducts { get; set; }

        public DbSet<ProductPhoto> ProductPhotos { get; set; }


    }
}