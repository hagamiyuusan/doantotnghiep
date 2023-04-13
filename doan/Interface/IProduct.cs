using doan.Entities;

namespace doan.Interface
{
    public interface IProduct
    {
        public Task<Product> getAllProduct();
        public Task<List<Product>> getProductsById(int id);
        public Task<bool> deleteProduct(int id);
        public Task<bool> editProduct(int id);
        public Task<Product> createProduct(Product product);
        
    }
}
