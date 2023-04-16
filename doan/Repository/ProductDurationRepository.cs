using doan.DTO.ProductDuration;
using doan.EF;
using doan.Entities;
using doan.Interface;

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

        public Task<bool> deleteProductDuration(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> editProductDuration(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDuration> getAllProductDuration()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDuration>> getProductDurationsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
