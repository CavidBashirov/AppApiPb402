using AppApi.Data;
using AppApi.DTOs.Employee;
using AppApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,
                                  IMapper mapper,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(_mapper.Map<List<EmployeeDto>>(await _context.Employees.ToListAsync()));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "images", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.UploadImage.CopyToAsync(stream);
            }

            var model = _mapper.Map<Employee>(request);
            model.Image = fileName;
            await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            _context.Employees.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(data));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EmployeeEditDto request)
        {
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            _mapper.Map(request, data);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            var datas= await _context.Employees.Where(m=>m.FullName.Trim().ToLower().Contains(searchText.Trim().ToLower())).ToListAsync();
            return Ok(_mapper.Map<List<EmployeeDto>>(datas));
        }

        [HttpGet]
        public async Task<IActionResult> SortByDate([FromQuery] string? order)
        {
            List<Employee> datas = new();
            switch (order)
            {
                case "desc":
                    datas = await _context.Employees.OrderByDescending(m => m.CreateDate).ToListAsync();
                    break;
                case "asc":
                    datas = await _context.Employees.OrderBy(m => m.CreateDate).ToListAsync();
                    break;
                default:
                    datas = await _context.Employees.ToListAsync();
                    break;
            }
            return Ok(_mapper.Map<List<EmployeeDto>>(datas));
            
        }
    }
}
