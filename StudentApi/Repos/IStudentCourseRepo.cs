using StudentApi.Models;

namespace StudentApi.Repos
{
    public interface IStudentCourseRepo
    {
        Task EnrollStudentToCourse(int studentId, int courseId);
        Task RemoveStudentFromCourse(int studentId, int courseId);
        Task<List<Course>> GetCoursesWithEnrolledStudentsAsync(List<Student> students, List<Course> courses);
    }
}
