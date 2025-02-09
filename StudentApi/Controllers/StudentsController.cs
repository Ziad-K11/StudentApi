using Microsoft.AspNetCore.Mvc;
using StudentApi.Repos;
using StudentApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _repo;

        public StudentsController(IStudent repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _repo.GetStudentsAs();
            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _repo.GetStudentAs(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newStudent = await _repo.AddStudentAs(student);
                return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != student.Id)
            {
                return BadRequest("ID's don't match");
            }
            var oldStudent = await _repo.GetStudentAs(id);
            if (oldStudent == null)
            {
                return NotFound();
            }
            await _repo.UpdateStudentAs(student);
            return NoContent();
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _repo.GetStudentAs(id);
            if (student == null)
            {
                return NotFound();
            }

            await _repo.DeleteStudentAs(id);

            return NoContent();
        }
    }
}
