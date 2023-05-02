using doan.DTO.ProductDuration;
using doan.EF;
using doan.Interface;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDurationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductDuration _productDuration;

        public ProductDurationController(ApplicationDbContext context, IProductDuration productDuration)
        {
            _context = context;
            _productDuration = productDuration;
        }
        [HttpGet]
        public async Task<IActionResult> getAllProductDuration()
        {
            var result = _productDuration.getAllProductDuration();
            return Ok(new JsonResult(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getProductDurationById([FromRoute] int id)
        {
            var result = _productDuration.getProductDurationsById(id);
            return Ok(new JsonResult(result));
        }
        [HttpPost]
        public async Task<IActionResult> createProductDuration([FromBody] ProductDurationCreateRequest request)
        {
            var result = await _productDuration.createProductDuration(request);
            return Ok(new JsonResult(result));
        }
        // [HttpPut]
        // public async Task<IActionResult> editProductDuration([FromBody] ProductDurationEditRequest request)
        // {
        //     var result = await _productDuration.editProductDuration(request);
        //     return Ok(new JsonResult(result));
        // }
        [HttpDelete("id")]
        public async Task<IActionResult> deleteProductDuration([FromRoute] int id)
        {
            var result = await _productDuration.deleteProductDuration(id);
            return Ok(new JsonResult(result));
        }
    }
}
