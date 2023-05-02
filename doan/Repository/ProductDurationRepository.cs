using doan.DTO.ProductDuration;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace doan.Repository
{
    public class ProductDurationRepository : IProductDuration
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public ProductDurationRepository(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<int> createProductDuration(ProductDurationCreateRequest request)
        {
            var product = await _context.Products.FindAsync(request.productId);
            var duration = await _context.Durations.FindAsync(request.durationId);

            ProductDuration productDurationEntity = new ProductDuration
            {
                duration = duration,
                product = product,
                price = request.price,
            };
            await _context.ProductDurations.AddAsync(productDurationEntity);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> deleteProductDuration(int id)
        {
            var productDuration = await _context.ProductDurations.FindAsync(id);

            _context.ProductDurations.Remove(productDuration);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> editProductDuration(int id, ProducDurationEditRequest request)
        {
            var productDuration = await _context.ProductDurations.FindAsync(id);

            if (productDuration == null)
            {
                return 0;
            }
            productDuration.price = request.price;
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<ProductDuration>> getAllProductDuration()
        {
            var result = await _context.ProductDurations.ToListAsync();
            return result;
        }

        public async Task<ProductDuration> getProductDurationsById(int id)
        {
            return await _context.ProductDurations.FindAsync(id);
        }
    }
}
