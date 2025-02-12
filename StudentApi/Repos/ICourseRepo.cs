using StudentApi.Models;

namespace StudentApi.Repos
{
    public interface ICourseRepo
    {
        Task<List<Course>> GetCoursesAsync();
        Task<List<Course>> GetCoursesAsync(int id);
        Task EnrollStudentToCourse(int studentId, int courseId);
        Task RemoveStudentFromCourse(int studentId, int courseId);
    }
}
