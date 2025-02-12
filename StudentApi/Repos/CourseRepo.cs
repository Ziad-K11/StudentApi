using StudentApi.Models;

namespace StudentApi.Repos
{
    public class CourseRepo : ICourseRepo
    {
        private readonly List<Course> courses;
        private readonly List<StudentCourse> studentCourses;
        private readonly IStudentRepo _studentRepo;
        private readonly IStudentCourseRepo _studentCourseRepo;

        public CourseRepo(IStudentRepo studentRepo, IStudentCourseRepo studentCourseRepo)
        {
            courses = new List<Course>
            {
                new Course { Id = 1, Name = "Software Engineering" },
                new Course { Id = 2, Name = "Object-Oriented Programming" },
                new Course { Id = 3, Name = "Parallel Programming" },
                new Course { Id = 4, Name = "Image Processing" },
                new Course { Id = 5, Name = "Mobile Development", }
            };
            studentCourses = new List<StudentCourse>();
            _studentCourseRepo = studentCourseRepo;
            _studentRepo = studentRepo;
        }

        public async  Task<List<Course>> GetCoursesAsync()
        {
            return await Task.FromResult(courses);
        }

        public Task EnrollStudentToCourse(int studentId, int courseId)
        {
            var studentCourse = new StudentCourse { StudentId = studentId, CourseId = courseId };
            studentCourses.Add(studentCourse);
            return Task.CompletedTask;
        }

        public Task RemoveStudentFromCourse(int studentId, int courseId)
        {
            var studentCourse = studentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (studentCourse != null)
            {
                studentCourses.Remove(studentCourse);
            }
            return Task.CompletedTask;
        }

        public async Task<List<StudentCourse>> GetStudentCourses()
        {
            
            var students = await _studentRepo.GetStudentsAsync(); 
            var courses = await GetCoursesAsync();      
            var studentCourses = await _studentCourseRepo.GetStudentCoursesAsync(); 

            
            var result = courses
                .Join(
                    studentCourses,
                    course => course.Id,                    
                    studentCourse => studentCourse.CourseId, 
                    (course, studentCourse) => new { course, studentCourse } 
                )
                .Join(
                    students,
                    courseStudentCourse => courseStudentCourse.studentCourse.StudentId, 
                    student => student.Id,                                             
                    (courseStudentCourse, student) => new StudentCourse
                    {
                        CourseId = courseStudentCourse.course.Id,
                        StudentId = student.Id
                    }
                )
                .ToList();

            return result;
        }
    }
}
