using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StudentApi.Models;


namespace StudentApi.Repos
{
    public class StudentRepo : IStudent
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

        public async Task<List<Student>> GetStudentsAs()
        {
            var query = from student in students
                        select student;
            return await Task.FromResult(query.ToList());
        }
        public async Task<Student> GetStudentAs(int id)
        {
            var query = from student in students
                        where student.Id == id
                        select student;
            return await Task.FromResult(query.FirstOrDefault());
        }

        public async Task<Student> AddStudentAs(Student student)
        {
            var query = from s in students
                        where s.Id == student.Id
                        select s;
            bool exist = false;
            foreach (var s in query)
            {
                exist = true;
                break;
            }
            if (exist)
            {
                throw new ArgumentException("ID already exists");
            }
            students.Add(student);
            return await Task.FromResult(student);
        }
        public async Task UpdateStudentAs(Student NewStudent)
        {
            var query = from student in students
                        where student.Id == NewStudent.Id
                        select student;
            Student updatingStudent = null;
            foreach (var student in query)
            {
                updatingStudent = student;
                break;
            }
            if (updatingStudent != null)
            {
                updatingStudent.Name = NewStudent.Name;
                updatingStudent.Grade = NewStudent.Grade;
            }
            await Task.CompletedTask;
        }
        public async Task DeleteStudentAs(int id)
        {
            var query = from student in students
                        where student.Id == id
                        select student;
            Student deletedStudent = null;
            foreach (var student in query)
            {
                deletedStudent = student;
                break;
            }
            if (deletedStudent != null)
            {
                students.Remove(deletedStudent);
            }
            await Task.CompletedTask;
        }
    }
}
