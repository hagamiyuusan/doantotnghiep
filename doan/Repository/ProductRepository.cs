using doan.DTO.Duration;
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

        public async Task<int> createProduct(ProductCreateRequest product)
        {
            var typeProduct = await _context.typeProducts.FindAsync(product.typeProductId);
            
            var desProduct = new Product
            {
                Name = product.Name,
                Created = product.Created,
                API_URL = product.API_URL,
                typeProduct = typeProduct

            };
            await _context.Products.AddAsync(desProduct);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> deleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();
            return result;

        }

        public async Task<int> editProduct(ProductEditRequest request)
        {
            var product = await _context.Products.FindAsync(request.productId);
            product.Name = request.name;
            product.API_URL = request.API_URL;
            var result = await  _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<ProductView>> getAllProduct()
        {

            var listProductView = await _context.Products.Include(x => x.productDurations)
                                  .ThenInclude(b => b.duration).Select(product => new ProductView
                                  {
                                      Name = product.Name,
                                      id = product.Id,
                                      durations = product.productDurations.Select(
                                          pd => new DurationView
                                          {
                                              original_duration_id = pd.duration.Id,
                                              id = pd.Id,
                                              Day = pd.duration.day,
                                              Price = pd.price
                                          }).ToList()
                                  }).ToListAsync();
            return listProductView;
        }

        public async Task<ProductView> getProductsById(int id)
        {
            return await _context.Products.Include(x => x.productDurations)
                .ThenInclude(b => b.duration).Where(x=>x.Id==id).Select(
                product => new ProductView
                {
                    Name = product.Name,
                    id = product.Id,
                    durations = product.productDurations.Select(
                                          pd => new DurationView
                                          {
                                              original_duration_id = pd.duration.Id,
                                              id = pd.Id,
                                              Day = pd.duration.day,
                                              Price = pd.price
                                          }).ToList()
                }
                )
                .FirstAsync();
        }


    }
}
