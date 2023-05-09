using doan.DTO.Product;
using doan.Entities;
using doan.Wrapper;
using doan.DTO;
namespace doan.Interface
{
    public interface IProduct
    {
        public Task<(List<ProductGet>, PaginationFilter, int)> getAllProduct(PaginationFilter filter);
        public Task<Product> getProductsById(int id);
        public Task<int> deleteProduct(int id);
        public Task<int> editProduct(ProductEditRequest request );
        public Task<int> createProduct(ProductCreateRequest product);
        

    }
}
