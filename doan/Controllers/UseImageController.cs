using doan.DTO;
using doan.DTO.UseImage;
using doan.EF;
using doan.Entities;
using doan.Helpers;
using doan.Interface;
using doan.Services;
using doan.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUseImage _useImage;
        private readonly IUriService _IUriService;

        public UseImageController(ApplicationDbContext context, IUseImage useImage, IUriService iUriService)
        {
            _context = context;
            _useImage = useImage;
            _IUriService = iUriService;
        }

        [HttpGet]
        public async Task<IActionResult> gettAllImage([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            var result = await _useImage.getAllUser(filter);
            foreach (var img in result.Item1)
            {
                img.path = Url.Content($"~/images/{img.path}");

            }
            var pagedReponse = PaginationHelper.CreatePagedReponse<DetailImageVM>(result.Item1, result.Item2, result.Item3, _IUriService, route);

            return new JsonResult(pagedReponse);
        }
    }
}
