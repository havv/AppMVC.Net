
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMvc.Net.Models;

[Table("ProductPhoto")]
public class ProductPhoto 
{
    [Key]
   public int Id { get; set; }
   
   //abc.png, 123.jpg
   //Các photo này sẽ được lưu vào thư mục Uploads được cấu hình để lưu ảnh và ảnh đc truy cập theo đường dẫn /contents (xem file Program.cs)
   // /contents/Products/abc.png
   public string  FileName { get; set; }

   public int  ProductId { get; set; }

   [ForeignKey("ProductId")]
   public Product Product { get; set; }

}