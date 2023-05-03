using doan.DTO.Subscription;
using doan.DTO.TypeProduct;
using doan.EF;
using doan.Entities;
using doan.Helpers;
using doan.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProductController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly ITypeProduct _typeProduct;

        public TypeProductController(ApplicationDbContext context, ITypeProduct typeProduct)
        {
            _context = context;
            _typeProduct = typeProduct;
        }
        [HttpGet]
        public async Task<IActionResult> getAllTypeProduct()
        {
            var result = await _typeProduct.getAllTypeProduct();
            return Ok(new JsonResult(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getTypeProductById([FromRoute] int id)
        {
            var result = await _typeProduct.getTypeProductById(id);
            return Ok(new JsonResult(result));
        }
        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> createTypeProduct([FromBody] TypeProductCreateRequest request)
        {
            var result = await _typeProduct.createTypeProduct(request);
            return Ok(new JsonResult(result));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> deleteTypeProduct([FromRoute(Name = "id")] int id)
        {
            var result = await _typeProduct.deleteTypeProduct(id);
            return Ok(new JsonResult(result));   
    }
    }
}