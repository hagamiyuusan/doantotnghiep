using doan.DTO.TypeProduct;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.EntityFrameworkCore;

namespace doan.Repository
{
    public class TypeProductRepository : ITypeProduct
    {
        private readonly ApplicationDbContext _context;

        public TypeProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> createTypeProduct(TypeProductCreateRequest request)
        {
            var typeProductCreate = new TypeProduct
            {
                name = request.name
            };
            await _context.typeProducts.AddAsync(typeProductCreate);
            var result =  await _context.SaveChangesAsync();
            return result;
        }


        public async Task<int> deleteTypeProduct(int id)
        {
            var objectToDelete = await _context.typeProducts.FindAsync(id);
            if (objectToDelete == null)
            {
                return 0;
            }
            _context.typeProducts.Remove(objectToDelete);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> editTypeProduct(int id, TypeProductCreateRequest request)
        {
            var typeProduct = await _context.typeProducts.FindAsync(id);

            if (typeProduct == null)
            {
                return 0;
            }
            typeProduct.name = request.name;
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<TypeProduct>> getAllTypeProduct()
        {
            return await _context.typeProducts.ToListAsync();
        }

        public async Task<TypeProduct> getTypeProductById(int id)
        {
            return await _context.typeProducts.FindAsync(id);
        }



    }
}
