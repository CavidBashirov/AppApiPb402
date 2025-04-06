using AppApi.Data;
using AppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student request)
        {
            await _context.Students.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (data is null) return NotFound();
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var data = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (data is null) return NotFound();
            _context.Students.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, Student request )
        {
            var data = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (data is null) return NotFound();
            data.FullName = request.FullName ?? data.FullName;
            data.Email = request.Email ?? data.Email;   
            data.Age = request.Age ?? data.Age;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
