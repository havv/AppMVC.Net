using System.ComponentModel.DataAnnotations;
using AppMvc.Net.Models;

namespace AppMvc.Net.Areas.Blog.Models;
public class CreatePostModel
{
    public int PostId { get; set; }

    [Display(Name = "Chuyên mục")]
    public int[] CategoryIds { get; set; }

    [Required(ErrorMessage = "Phải có tiêu đề bài viết")]
    [Display(Name = "Tiêu đề")]
    [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
    public string Title { get; set; }

    [Display(Name = "Mô tả ngắn")]
    public string Description { get; set; }

    [Display(Name = "Chuỗi định danh (url)", Prompt = "Nhập hoặc để trống tự phát sinh theo tên")]
    [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
    [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9]")]
    public string Slug { get; set; }
    
    [Display(Name = "Nội dung")]
    public string Content { get; set; }

    [Display(Name = "Xuất bản")]
    public bool Published { get; set; }

    // [Display(Name = "Tác giả")]
    // public string AuthorId { get; set; }

    
}