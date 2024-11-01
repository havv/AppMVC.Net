using System.ComponentModel.DataAnnotations.Schema;

namespace AppMvc.Net.Models;

[Table("ProductCategoryProduct")]
public class ProductCategoryProduct
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    [ForeignKey("CategoryId")]
    public CategoryProduct Category { get; set; }
}