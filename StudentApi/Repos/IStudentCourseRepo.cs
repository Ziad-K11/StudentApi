using StudentApi.Models;

namespace StudentApi.Repos
{
    public interface IStudentCourseRepo
    {
        Task<List<StudentCourse>> EnrollStudentAsync(int StudentId,int CourseId);
        Task<List<StudentCourse>> RemoveStudentAsync(int StudentId, int CourseId);
        Task<List<Course>> GetCoursesWithEnrolledStudentsAsync(List<Student> students, List<Course> courses);
    }
}
