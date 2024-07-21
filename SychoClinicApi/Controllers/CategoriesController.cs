using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SychoClinicApi.Dtos;
using SychoClinicApi.Models;
using System.Data;

namespace SychoClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Category = await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
            return Ok(Category);
        }

        [HttpGet]
        public async Task<IActionResult> GetTypeAsync()
        {
            var CategoryType = await _context.CategoriesArticles.OrderBy(c => c.TypeName).ToListAsync();
            return Ok(CategoryType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Details")]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryDto dto)
        {
            using var dataStream = new MemoryStream();
            await dto.Image.CopyToAsync(dataStream);

            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    CategoryName = dto.CategoryName,
                    Description = dto.Description,
                    Image = dataStream.ToArray(),
                    Symptoms = dto.Symptoms,
                    Treatment = dto.Treatment,
                    CategoriesArticleId = dto.CategoriesArticleId

                };

                await _context.Categories.AddAsync(category);
                _context.SaveChanges();
                return Ok(category);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Detials/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var record = await _context.Categories.FindAsync(id);
            if (record == null)
                return NotFound($"No Article with ID : {id}");

            _context.Categories.Remove(record);
            _context.SaveChanges();

            return Ok(record);
        }
    }
}
