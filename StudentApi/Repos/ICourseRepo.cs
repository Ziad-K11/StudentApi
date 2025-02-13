using StudentApi.Models;

namespace StudentApi.Repos
{
    public interface ICourseRepo
    {
        Task<List<Course>> GetCoursesAsync();
        Task<Course> GetCourseAsync(int id);

    }
}
