using doan.DTO.ProductDuration;
using doan.Entities;

namespace doan.Interface
{
    public interface IProductDuration
    {
        public Task<List<ProductDuration>> getAllProductDuration();
        public Task<ProductDuration> getProductDurationsById(int id);
        public Task<bool> deleteProductDuration(int id);
        public Task<bool> editProductDuration(int id);
        public Task<ProductDuration> createProductDuration(ProductDurationCreateRequest request);
    }
}
