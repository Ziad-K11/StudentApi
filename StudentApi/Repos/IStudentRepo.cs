using StudentApi.Models;


namespace StudentApi.Repos
{
    public interface IStudentRepo
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
