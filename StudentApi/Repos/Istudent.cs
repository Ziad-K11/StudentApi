using StudentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApi.Repos
{
    public interface IStudent
    {
        Task<List<Student>> GetStudentsAs();
        Task<Student> GetStudentAs(int id);
        Task<Student> AddStudentAs(Student student);
        Task UpdateStudentAs(Student student);
        Task DeleteStudentAs(int id);
    }
}
