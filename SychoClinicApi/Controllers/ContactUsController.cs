using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SychoClinicApi.Dtos;
using SychoClinicApi.Models;

namespace SychoClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var record = await _context.ContectUs.ToListAsync();
            return Ok(record);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var record = await _context.ContectUs.FindAsync(id);

            if (record == null)
                return NotFound($"No Massege from user with ID: {id}");

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ContactUsDto dto)
        {
            if (ModelState.IsValid)
            {
                ContectUs record = new ContectUs
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Message = dto.Message
                };

                await _context.ContectUs.AddAsync(record);
                _context.SaveChanges();

                return Ok(record);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ContactUsDto dto)
        {
            var record = await _context.ContectUs.FindAsync(id);

            if (record == null)
                return NotFound($"No Massege from user with ID: {id}");

            if (ModelState.IsValid)
            {
                record.FullName = dto.FullName;
                record.Email = dto.Email;
                record.Message = dto.Message;

                _context.SaveChanges();
                return Ok(record);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var record = await _context.ContectUs.FindAsync(id);

            if (record == null)
                return NotFound($"No Massege from user with ID: {id}");

            _context.ContectUs.Remove(record);
            _context.SaveChanges();
            return Ok(record);
        }
    }
}
