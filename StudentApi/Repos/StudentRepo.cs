using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StudentApi.Models;


namespace StudentApi.Repos
{
    public class StudentRepo : IStudentRepo
    {
        private readonly List<Student> students;
        public StudentRepo()
        {
            students = new List<Student>
            {
                new Student { Id = 1, Name = "Ziad Khaled", Grade = 100 },
                new Student { Id = 2, Name = "Zeina Khaled", Grade = 85 },
                new Student { Id = 3, Name = "Muhammed Ahmed", Grade = 70 },
                new Student { Id = 4, Name = "Aly Attia", Grade = 50 }
            };

        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await Task.FromResult(students.ToList());
        }
        public async Task<Student> GetStudentAsync(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            return await Task.FromResult(student);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            if (students.Any(s => s.Id == student.Id))
            {
                throw new ArgumentException("A student with the given ID already exists.");
            }

            students.Add(student);
            return await Task.FromResult(student);
        }
        public async Task UpdateStudentAsync(Student NewStudent)
        {
            var existingStudent = students.FirstOrDefault(s => s.Id == NewStudent.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = NewStudent.Name;
                existingStudent.Grade = NewStudent.Grade;
            }
            await Task.CompletedTask;
        }
        public async Task DeleteStudentAsync(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
            }
            await Task.CompletedTask;
        }
    }
}
