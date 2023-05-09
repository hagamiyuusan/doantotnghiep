using doan.DTO.Product;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using doan.Wrapper;
using Microsoft.AspNetCore.Identity;
using doan.DTO;
namespace doan.Repository

{
    public class ProductRepository : IProduct
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public ProductRepository(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<int> createProduct(ProductCreateRequest product)
        {
            var typeProduct = await _context.typeProducts.FindAsync(product.typeProductId);
            
            var desProduct = new Product
            {
                Name = product.Name,
                Created = product.Created,
                API_URL = product.API_URL,
                typeProduct = typeProduct

            };
            await _context.Products.AddAsync(desProduct);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> deleteProduct(int id)
        {
            var product = await this.getProductsById(id);

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();
            return result;

        }

        public async Task<int> editProduct(ProductEditRequest request)
        {
            var product = await _context.Products.FindAsync(request.productId);
            product.Name = request.name;
            product.API_URL = request.API_URL;
            var result = await  _context.SaveChangesAsync();
            return result;
        }

        public async Task<(List<ProductGet>,PaginationFilter,int)> getAllProduct(PaginationFilter filter)
        {
             List<ProductGet> result = new List<ProductGet>();

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.key);

            if (!String.IsNullOrEmpty(filter.key))
            {   
                var productFilter=await _context.Products.Where(x =>x.Id.ToString() == filter.key)
                                                  .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                                                  .Take(validFilter.PageSize)
                                                  .ToListAsync();
                // var productFilter = await _productManager.Users.Where(x =>x.Id.ToString() == filter.key)
                //     .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                //     .Take(validFilter.PageSize)
                //     .ToListAsync();

                var countFilter = await _context.Products.Where(x => x.Id.ToString() == filter.key).CountAsync();


                foreach (var product in productFilter)
                {
                    var tempProduct = new ProductGet
                    {
                        Id = product.Id,
                        Name = product.Name
                    };
                    result.Add(tempProduct);
                }

                return (result, validFilter, countFilter);
            }
            var listProduct=await _context.Products.Skip((filter.PageNumber - 1) * validFilter.PageSize)
                                                    .Take(validFilter.PageSize)
                                                    .ToListAsync();
            // var listProduct = await _productManager.Users
            //         .Skip((filter.PageNumber - 1) * validFilter.PageSize)
            //         .Take(validFilter.PageSize)
            //         .ToListAsync();
            foreach (var product in listProduct)
            {
                var tempProduct = new ProductGet
                {
                    Id = product.Id,
                    Name = product.Name
                };
                result.Add(tempProduct);
            }
            var count = await _context.Products.CountAsync();
            return (result, validFilter, count);
        }

        public async Task<Product> getProductsById(int id)
        {
            return await _context.Products.FindAsync(id);
        }


    }
}
