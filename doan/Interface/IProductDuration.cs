using doan.Entities;

namespace doan.Interface
{
    public interface IProductDuration
    {
        public Task<ProductDuration> getAllProductDuration();
        public Task<List<ProductDuration>> getProductDurationsById(int id);
        public Task<bool> deleteProductDuration(int id);
        public Task<bool> editProductDuration(int id);
        public Task<ProductDuration> createProductDuration(ProductDuration product);
    }
}
