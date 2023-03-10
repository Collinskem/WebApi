using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) {
            _context = context;
        }
        public Product CreateProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            return null;
        }

        public Task<Product> DeleteProduct(int id)
        {
            var result = _context.Products.FirstOrDefault(p => p.id == id);
            if (result != null)
            {
                _context.Products.Remove(result);
                _context.SaveChanges();
                //return result;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p=>p.id==id);
        }

        public async Task<Product> PutProduct(int id, Product product)
        {
            var result =  _context.Products.FirstOrDefault(p => p.id == id);
            if (result != null)
            {
                result.title = product.title;
                result.description = product.description;
                result.price = product.price;
                result.discountPercentage = product.discountPercentage;
                result.rating = product.rating;
                result.stock = product.stock;
                result.brand = product.brand;
                result.category = product.category;
                

                _context.Products.Update(result);
                _context.SaveChanges();
                return result;
            }
            return null;

        }
    }
}
