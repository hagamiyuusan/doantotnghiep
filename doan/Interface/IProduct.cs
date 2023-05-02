using doan.DTO.Product;
using doan.Entities;

namespace doan.Interface
{
    public interface IProduct
    {
        public Task<List<Product>> getAllProduct();
        public Task<Product> getProductsById(int id);
        public Task<int> deleteProduct(int id);
        public Task<int> editProduct(ProductEditRequest request );
        public Task<int> createProduct(ProductCreateRequest product);
        
    }
}
