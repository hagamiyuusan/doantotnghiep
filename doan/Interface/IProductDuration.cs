using doan.DTO.ProductDuration;
using doan.Entities;

namespace doan.Interface
{
    public interface IProductDuration
    {
        public Task<List<ProductDuration>> getAllProductDuration();
        public Task<ProductDuration> getProductDurationsById(int id);
        public Task<int> deleteProductDuration(int id);
        public Task<int> editProductDuration(int id, ProducDurationEditRequest request);
        public Task<int> createProductDuration(ProductDurationCreateRequest request);
    }
}
