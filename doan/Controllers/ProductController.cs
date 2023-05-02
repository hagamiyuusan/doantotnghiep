using doan.DTO.API;
using doan.DTO.Product;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IuseImageToText _userImageToText;
        private readonly ApplicationDbContext _context;

        public ProductController(IProduct product, IuseImageToText userImageToText, ApplicationDbContext context)
        {
            _product = product;
            _userImageToText = userImageToText;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> addProduct(ProductCreateRequest product)
        {

            var result = await _product.createProduct(product);
            return result;
        }

        [HttpPost("imagecaptioning")]
        public async Task<IActionResult> useImageCaptioning([FromForm] UploadImageToText request)
        {

            var result = await _userImageToText.useProduct(request);
            return Ok(new
            {
                code = 200,
                result  = result
            });
        }
        [HttpPost("subscription")]
        public async Task<IActionResult> useImageCaptiongWithSubscription([FromForm] UploadImageToText request)
        {
            if (String.IsNullOrEmpty(request.token))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Có lỗi xảy ra, vui lòng kiểm tra lại"
                });
            }
            var product = await _context.Products.Where(x => x.Id == request.idProduct).FirstAsync();
            if (product == null)
            {
                return BadRequest(new
                {
                    code = 404,
                    message = "Có lỗi xảy ra, vui lòng kiểm tra lại"
                });
            }
            var subscription = await _context.Subscriptions.Where(x => x.token == request.token)
                .Include(a => a.productDuration)
                .ThenInclude(b => b.product)
                .FirstAsync();
            if (subscription == null)
            {
                return BadRequest(new
                {
                    code = 404,
                    message = "Có lỗi xảy ra, vui lòng kiểm tra lại"
                });
            }
            if (subscription.dueDate < DateTime.Now)
            {
                return BadRequest(new
                {
                    code = 401,
                    message = "Có lỗi xảy ra, vui lòng kiểm tra lại"
                });
            }

            var result = await _userImageToText.useProduct(request);
            return Ok(new
            {
                code = 200,
                result = result
            });
        }
        //[HttpPost("images")]
        //public async Task<string> uploadImage([FromForm] UploadImageRequest request)
        //{
        //    var result = await _useProductImageCaptioning.uploadFile(request);
        //    return result;
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> deletedProduct([FromRoute(Name="id")] int id)
        {
            var result = await _product.deleteProduct(id);
            return new JsonResult(result);
            
            
        }

    }
}
