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
        private readonly IStudentRepo _repo;
        private readonly ICourseRepo _courseRepo;
        private readonly IStudentCourseRepo _studentCourseRepo;


        public StudentsController(IStudentRepo repo, ICourseRepo courseRepo,IStudentCourseRepo studentCourseRepo)
        {
            _repo = repo;
            _courseRepo = courseRepo;
            _studentCourseRepo = studentCourseRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _repo.GetStudentsAsync();
            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _repo.GetStudentAsync(id);

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
            var newStudent = await _repo.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
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
            var oldStudent = await _repo.GetStudentAsync(id);
            if (oldStudent == null)
            {
                return NotFound();
            }
            await _repo.UpdateStudentAsync(student);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _repo.GetStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _repo.DeleteStudentAsync(id);

            return NoContent();
        }
        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent([FromQuery] int studentId, [FromQuery] int courseId)
        {
            if (studentId <= 0 || courseId <= 0)
                return BadRequest("Invalid student or course ID.");

            var student = await _repo.GetStudentAsync(studentId);
            var course = await _courseRepo.GetCoursesAsync(courseId);

            if (student == null)
                return NotFound("Student not found.");
            if (course == null)
                return NotFound("Course not found.");

            await _studentCourseRepo.EnrollStudentAsync(studentId, courseId);
            return CreatedAtAction(nameof(GetCoursesWithEnrolledStudents), new { studentId, courseId }, "Student successfully enrolled.");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveStudent([FromQuery] int studentId, [FromQuery] int courseId)
        {
            if (studentId <= 0 || courseId <= 0)
                return BadRequest("Invalid student or course ID.");

            var student = await _repo.GetStudentAsync(studentId);
            var course = await _courseRepo.GetCoursesAsync(courseId);

            if (student == null)
                return NotFound("Student not found.");
            if (course == null)
                return NotFound("Course not found.");

            await _studentCourseRepo.RemoveStudentAsync(studentId, courseId);
            return NoContent(); 
        }
        [HttpGet("view")]
        public async Task<IActionResult> GetCoursesWithEnrolledStudents()
        {
            var students = await _repo.GetStudentsAsync();
            var courses = await _courseRepo.GetCoursesAsync();
            var result = await _studentCourseRepo.GetCoursesWithEnrolledStudentsAsync(students, courses);

            return Ok(result);
        }
    }
}
