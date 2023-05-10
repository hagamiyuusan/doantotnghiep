using doan.DTO.API;
using doan.DTO.Duration;
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
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<Product>> addProduct(ProductCreateRequest product)
        {
            var result = await _product.createProduct(product);
            if (result == 0)
            {
                return BadRequest("Không thể thực hiện");
            }
            return Ok("Thực hiện thành công");
        }
        [HttpPost("imagecaptioning")]
        public async Task<IActionResult> useImageCaptioning([FromForm] UploadImageToText request)
        {

            var result = await _userImageToText.useProduct(request);
            return Ok(new
            {
                code = 200,
                result = result
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
                .Include(a => a.product)
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
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> deletedProduct([FromRoute(Name = "id")] int id)
        {
            var result = await _product.deleteProduct(id);
            if (result == 0)
            {
                return BadRequest("Không thể thực hiện");
            }
            return Ok("Thực hiện thành công");

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> editProduct([FromRoute(Name = "id")] int id,[FromBody] ProductEditRequest request)
        {
            request.productId = id;
            var result = await _product.editProduct(request);
            if (result == 0)
            {
                return BadRequest("Không thể thực hiện");
            }
            return Ok("Thực hiện thành công");
        }
        [HttpGet]
        public async Task<IActionResult> getAllProduct()
        {
            return Ok(new
            {
                status = 200,
                value = await _product.getAllProduct()
            }) ;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getProductById([FromRoute(Name = "id")] int id)
        {
            return Ok(new
            {
                status = 200,
                value = await _context.Products.FindAsync(id)
            });
        }


    }
}
