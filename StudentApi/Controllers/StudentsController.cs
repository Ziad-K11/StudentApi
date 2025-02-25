using Microsoft.AspNetCore.Mvc;
using StudentApi.Repos;
using StudentApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepo _repo;
        private readonly ICourseRepo _courseRepo;
        private readonly IStudentCourseRepo _studentCourseRepo;


        public StudentsController(IStudentRepo repo, ICourseRepo courseRepo, IStudentCourseRepo studentCourseRepo)
        {
            _repo = repo;
            _courseRepo = courseRepo;
            _studentCourseRepo = studentCourseRepo;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _repo.GetStudentsAsync();
            return Ok(students);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _repo.GetStudentAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newStudent = await _repo.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
        }


        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
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

        [HttpGet("courses")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCoursesAsync()
        {
            var courses = await _courseRepo.GetCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("courses/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourseAsync(int id)
        {
            var student = await _courseRepo.GetCourseAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        [HttpPost("enroll")]
        [Authorize]
        public async Task<IActionResult> EnrollStudentToCourse(int studentId, int courseId)
        {
            var student = await _repo.GetStudentAsync(studentId);
            var course = await _courseRepo.GetCourseAsync(courseId);

            if (student == null || course == null)
            {
                return NotFound("Student or course not found.");
            }

            await _studentCourseRepo.EnrollStudentToCourse(studentId, courseId);
            return Ok($"Student {student.Name} has been enrolled in course {course.Name}.");
        }

        [HttpDelete("remove")]
        [Authorize]
        public async Task<IActionResult> RemoveStudentFromCourse(int studentId, int courseId)
        {
            var student = await _repo.GetStudentAsync(studentId);
            var course = await _courseRepo.GetCourseAsync(courseId);

            if (student == null || course == null)
            {
                return NotFound("Student or course not found.");
            }

            await _studentCourseRepo.RemoveStudentFromCourse(studentId, courseId);
            return Ok($"Student {student.Name} has been removed from course {course.Name}.");
        }

        [HttpGet("courses-with-students")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCoursesWithEnrolledStudents()
        {
            var students = await _repo.GetStudentsAsync();
            var courses = await _courseRepo.GetCoursesAsync();

            var result = await _studentCourseRepo.GetCoursesWithEnrolledStudentsAsync(students, courses);
            return Ok(result);
        }



    }
}
