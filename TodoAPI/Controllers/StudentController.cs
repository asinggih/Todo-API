using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller {

        private readonly TodoContext _context;

        public StudentController(TodoContext context) {

            _context = context;

            if (_context.Student.Count() == 0) {

                _context.Student.Add(new Student {
                    f_name = "Ad",
                    l_name = "Ss",
                    dob = new DateTime(1991, 1, 28)
                });
                _context.SaveChanges();

            }

        }

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents() {

            return await _context.Student.ToListAsync();

        }

        // GET api/student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id) {

            var student = await _context.Student.FindAsync(id);

            if (student is null) {
                return NotFound();
            }

            return student;


        }

        // POST api/student
        [HttpPost]
        public async Task<ActionResult<Student>> PostNewStudent(Student student) {

            Console.WriteLine("student is " + student.f_name);
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student
            );
        }
        

        // PUT api/student/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/student/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
