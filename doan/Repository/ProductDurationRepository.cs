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

        public async Task<ProductDuration> createProductDuration(ProductDurationCreateRequest request)
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
            await _context.SaveChangesAsync();
            return productDurationEntity;
        }

        public async Task<bool> deleteProductDuration(int id)
        {
            var productDuration = await _context.ProductDurations.FindAsync(id);

            _context.ProductDurations.Remove(productDuration);
            var result = await _context.SaveChangesAsync();
            return (result == 1 ? true : false);
        }

        public Task<bool> editProductDuration(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDuration>> getAllProductDuration()
        {
            var result = await _context.ProductDurations.ToListAsync();
            return result;
        }

        public Task<ProductDuration> getProductDurationsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
