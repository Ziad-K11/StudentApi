using StudentApi.Models;

namespace StudentApi.Repos
{
    public class StudentCourseRepo: IStudentCourseRepo
    {
        private readonly List<StudentCourse> StudentCourses;

        public StudentCourseRepo()
        {
            StudentCourses = new List<StudentCourse>();
        }

        public async Task EnrollStudentAsync(int studentId, int courseId)
        {
            if (StudentCourses.Any(sc => sc.StudentId == studentId && sc.CourseId == courseId))
            {
                throw new ArgumentException("The student is already enrolled in this course.");
            }

            StudentCourses.Add(new StudentCourse { StudentId = studentId, CourseId = courseId });
            await Task.CompletedTask;
        }

        public async Task RemoveStudentAsync(int studentId, int courseId)
        {
            var enrollment = StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (enrollment == null)
            {
                throw new ArgumentException("The student is not enrolled in this course.");
            }

            StudentCourses.Remove(enrollment);
            await Task.CompletedTask;
        }
        public async Task<List<Course>> GetCoursesWithEnrolledStudentsAsync(List<Student> students, List<Course> courses)
        {
            var result = courses.Select(course => new Course
            {
                CourseId = course.Id,
                CourseName = course.Name,
                EnrolledStudents = students
                    .Where(student => StudentCourses.Any(sc => sc.CourseId == course.Id && sc.StudentId == student.Id))
                    .ToList()
            }).ToList();

            return await Task.FromResult(result);
        }

        public async Task<List<StudentCourse>> GetStudentCoursesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
