using doan.DTO.Product;
using doan.Entities;

namespace doan.Interface
{
    public interface IProduct
    {
        public Task<List<Product>> getAllProduct();
        public Task<Product> getProductsById(int id);
        public Task<bool> deleteProduct(int id);
        public Task<bool> editProduct(ProductEditRequest request );
        public Task<Product> createProduct(ProductCreateRequest product);
        
    }
}
