using doan.DTO.Product;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

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
            var typeProduct = await _context.typeProducts.FindAsync(product.typeProductId);
            
            var desProduct = new Product
            {
                Name = product.Name,
                Created = product.Created,
                API_URL = product.API_URL,
                typeProduct = typeProduct

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

        public async Task<bool> editProduct(ProductEditRequest request)
        {
            var product = await _context.Products.FindAsync(request.productId);
            product.Name = request.name;
            product.API_URL = request.API_URL;
            var result = await  _context.SaveChangesAsync();
            return (result == 1 ? true : false);
        }

        public async Task<List<Product>> getAllProduct()
        {
            var listProduct = await _context.Products.ToListAsync();
            return listProduct;
        }

        public async Task<Product> getProductsById(int id)
        {
            return await _context.Products.FindAsync(id);
        }


    }
}
