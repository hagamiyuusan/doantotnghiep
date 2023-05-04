﻿using doan.DTO.Duration;
using doan.EF;
using doan.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DurationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDuration _duration;

        public DurationController(ApplicationDbContext context, IDuration duration)
        {
            _context = context;
            _duration = duration;
        }
        [HttpGet]
        public async Task<IActionResult> getAllDuration()
        {
            var result =  await _duration.getAllDuration();
            return Ok(new JsonResult(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getDurationById([FromRoute] int id)
        {
            var result = await _duration.getDurationById(id);
            return Ok(new JsonResult(result));
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> createDuration([FromBody] DurationCreateRequest request)
        {
            var result = await _duration.createDuration(request);
            if (result == 0)
            {
                return BadRequest("Không thể thực hiện");
            }
            return Ok("Thực hiện thành công");
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> editDuration([FromBody] DurationEditRequest request)
        {
            var result = await _duration.editDuration(request);
            if (result == 0)
            {
                return BadRequest("Không thể thực hiện");
            }
            return Ok("Thực hiện thành công");
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDuration([FromRoute(Name = "id")] int id)
        {
            var result = await _duration.deleteDuration(id);

            if (result == 0)
            {
                return BadRequest("Không thể thực hiện");
            }
            return Ok("Thực hiện thành công");
        }
    }
}
