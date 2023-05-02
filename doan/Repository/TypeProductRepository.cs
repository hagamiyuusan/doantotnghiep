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

        public async Task<TypeProduct> createTypeProduct(TypeProductCreateRequest request)
        {
            var typeProductCreate = new TypeProduct
            {
                name = request.name
            };
            await _context.typeProducts.AddAsync(typeProductCreate);
            await _context.SaveChangesAsync();
            return typeProductCreate;
        }


        public async Task<bool> deleteTypeProduct(int id)
        {
            var objectToDelete = await _context.typeProducts.FindAsync(id);
            if (objectToDelete == null)
            {
                return false;
            }
            _context.typeProducts.Remove(objectToDelete);
            await _context.SaveChangesAsync();
            return true;
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
