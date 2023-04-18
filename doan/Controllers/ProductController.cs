using doan.DTO.API;
using doan.DTO.Product;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController
    {
        private readonly IProduct _product;
        private readonly IUseProductImageCaptioning _useProductImageCaptioning;
        private readonly ApplicationDbContext _context;

        public ProductController(IProduct product, IUseProductImageCaptioning useProductImageCaptioning, ApplicationDbContext context)
        {
            _product = product;
            _useProductImageCaptioning = useProductImageCaptioning;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> addProduct(ProductCreateRequest product)
        {

            var result = await _product.createProduct(product);
            return result;
        }
        [HttpPost("{id}")]
        public async Task<String> useProduct([FromRoute]int id, [FromForm] UploadImageRequest request)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            var API_URL = product.API_URL;

            var result = await _useProductImageCaptioning.useProduct(request,API_URL);
            return result;
        }
        //[HttpPost("images")]
        //public async Task<string> uploadImage([FromForm] UploadImageRequest request)
        //{
        //    var result = await _useProductImageCaptioning.uploadFile(request);
        //    return result;
        //}


    }
}
