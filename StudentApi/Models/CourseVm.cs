namespace StudentApi.Models
{
    public class CourseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> EnrolledStudents { get; set; }
    }
}
