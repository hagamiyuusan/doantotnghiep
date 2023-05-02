using doan.DTO.API;
using doan.DTO.TypeProduct;
using doan.Entities;

namespace doan.Interface
{
    public interface ITypeProduct
    {
        public Task<List<TypeProduct>> getAllTypeProduct();
        public Task<TypeProduct> getTypeProductById(int id);
        public Task<TypeProduct> createTypeProduct(TypeProductCreateRequest request);
        public Task<bool> deleteTypeProduct(int id);

    }
}
