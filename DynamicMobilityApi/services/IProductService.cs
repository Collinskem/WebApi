using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Product CreateProduct(Product product);
        Task<Product> DeleteProduct(int id);
        Task<Product> PutProduct(int id,Product product);

    }
}
