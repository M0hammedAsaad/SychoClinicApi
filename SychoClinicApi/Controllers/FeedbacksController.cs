using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SychoClinicApi.Dtos;
using SychoClinicApi.Models;

namespace SychoClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }


        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var FeedBack = await _context.FeedBacks.ToListAsync();
            return Ok(FeedBack);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreareAsync(FeedBackDto dto)
        {
            if (ModelState.IsValid)
            {
                FeedBack feedBack = new FeedBack
                {
                    Name = dto.Name,
                    Feedback = dto.Feedback
                };

                await _context.FeedBacks.AddAsync(feedBack);
                _context.SaveChanges();

                return Ok(feedBack);
            }

            return BadRequest(ModelState);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] FeedBackDto dto)
        {
            var feedBack = await _context.FeedBacks.FindAsync(id);

            if (feedBack == null)
                return NotFound($"No FeedBack was found by id : {id}");

            if (ModelState.IsValid)
            {
                feedBack.Name = dto.Name;
                feedBack.Feedback = dto.Feedback;

                _context.SaveChanges();
                return Ok(feedBack);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var feed = await _context.FeedBacks.FindAsync(id);

            if (feed == null)
                return NotFound($"No FeedBack found with ID : {id}");

            _context.FeedBacks.Remove(feed);
            _context.SaveChanges();

            return Ok(feed);
        }
    }
}
