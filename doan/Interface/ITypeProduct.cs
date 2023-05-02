using doan.DTO.API;
using doan.DTO.TypeProduct;
using doan.Entities;

namespace doan.Interface
{
    public interface ITypeProduct
    {
        public Task<List<TypeProduct>> getAllTypeProduct();
        public Task<TypeProduct> getTypeProductById(int id);
        public Task<int> createTypeProduct(TypeProductCreateRequest request);
        public Task<int> deleteTypeProduct(int id);
        public Task<int> editTypeProduct(int id, TypeProductCreateRequest request);

    }
}
