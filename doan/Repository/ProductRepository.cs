using doan.DTO.Product;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.EntityFrameworkCore;

namespace doan.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public ProductRepository(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<Product> createProduct(ProductCreateRequest product)
        {
            var desProduct = new Product
            {
                Name = product.Name,
                Created = product.Created
            };
            var result = await _context.Products.AddAsync(desProduct);
            await _context.SaveChangesAsync();
            return desProduct;
        }

        public async Task<bool> deleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null )
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<bool> editProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> getAllProduct()
        {
            var listProduct = await _context.Products.ToListAsync();
            return listProduct;
        }

        public Task<Product> getProductsById(int id)
        {
            throw new NotImplementedException();
        }


    }
}
