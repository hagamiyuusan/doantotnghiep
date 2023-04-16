using doan.DTO.API;
using doan.DTO.Product;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController
    {
        private readonly IProduct _product;
        private readonly IUseProductImageCaptioning _useProductImageCaptioning;


        public ProductController(IProduct product, IUseProductImageCaptioning useProductImageCaptioning)
        {
            _product = product;
            _useProductImageCaptioning = useProductImageCaptioning;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> addProduct(ProductCreateRequest product)
        {

            var result = await _product.createProduct(product);
            return result;
        }
        [HttpPost("id")]
        public async Task<String> useProduct([FromRoute] int id, [FromBody] ImageForCaptioningRequest request)
        {
            request.idService = id;
            var result = await _useProductImageCaptioning.useProduct(request);
            return result;
        }
        [HttpPost("images")]
        public async Task<bool> uploadImage([FromForm] UploadImageRequest request)
        {
            var result = await _useProductImageCaptioning.uploadFile(request);
            return result;
        }


    }
}
