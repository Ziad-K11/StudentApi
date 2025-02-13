using StudentApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Repos
{
    public class StudentCourseRepo : IStudentCourseRepo
    {
        private readonly List<StudentCourse> _studentCourses;

        public StudentCourseRepo()
        {
            _studentCourses = new List<StudentCourse>();
        }

        public async Task EnrollStudentToCourse(int studentId, int courseId)
        {
            await Task.Run(() =>
            {
                var alreadyEnrolled = _studentCourses.Any(sc => sc.StudentId == studentId && sc.CourseId == courseId);
                if (!alreadyEnrolled)
                {
                    _studentCourses.Add(new StudentCourse
                    {
                        StudentId = studentId,
                        CourseId = courseId
                    });
                }
            });
        }

        public async Task RemoveStudentFromCourse(int studentId, int courseId)
        {
            await Task.Run(() =>
            {

                var studentCourse = _studentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

                if (studentCourse != null)
                {
                    _studentCourses.Remove(studentCourse);
                }
            });
        }
        public async Task<List<Course>> GetCoursesWithEnrolledStudentsAsync(List<Student> students, List<Course> courses)
        {
            return await Task.Run(() =>
            {
                return courses.Select(course =>
                {
                    var enrolledStudents = _studentCourses
                        .Where(sc => sc.CourseId == course.Id)
                        .Select(sc => students.FirstOrDefault(student => student.Id == sc.StudentId))
                        .Where(student => student != null)
                        .ToList();


                    return new Course
                    {
                        Id = course.Id,
                        Name = course.Name,
                        EnrolledStudents = enrolledStudents
                    };
                }).ToList();
            });
        }


    }
}
