using API.Data;
using API.Entities;
using API.Models.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DataContext _context;

        public CoursesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _context.Courses;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _context.Courses.FirstOrDefault(p => p.Id == id);
            if (result == null) return NotFound($"Resource with {id} no more exists");
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Course
            {
                Name = model.Name,
                Code = model.Code,
            };

            _context.Courses.Add(entity);
            if (_context.SaveChanges() > 0)
            {
                return Created($"/api/courses/{entity.Id}", entity);
            }

            return BadRequest($"Error occured");
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] UpdateModel model)
        {
            var result = _context.Courses.FirstOrDefault(p => p.Id == id);
            if (result == null) return NotFound($"Resource with {id} no more exists");

            if (!ModelState.IsValid) return BadRequest(ModelState);


            result.Name = model.Name;
            result.Code = model.Code;

            _context.Courses.Update(result);
            if (_context.SaveChanges() > 0)
            {
                return NoContent();
            }

            return BadRequest($"Error occured");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _context.Courses.FirstOrDefault(p => p.Id == id);
            if (result == null) return NotFound($"Resource with {id} no more exists");

            _context.Courses.Remove(result);
            if (_context.SaveChanges() > 0)
            {
                return NoContent();
            }

            return BadRequest($"Error occured");
        }
    }
}
