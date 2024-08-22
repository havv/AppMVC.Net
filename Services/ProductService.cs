using AppMvc.Net.Models;

namespace AppMvc.Net.Services
{
    
public class ProductService : List<ProductModel>
{
    public ProductService()
    {
        this.AddRange([
            new ProductModel() {Id = 1, Name = "Iphone X", Price = 1000},
            new ProductModel() {Id = 2, Name = "Samsung Abc", Price = 800},

            new ProductModel() {Id = 3, Name = "Sony Xyz", Price = 500},

            new ProductModel() {Id = 4, Name = "Nokia 1280", Price = 200},


        ]);
    }
    
}


}
