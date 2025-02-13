using StudentApi.Models;

namespace StudentApi.Repos
{
    public class CourseRepo : ICourseRepo
    {
        private readonly List<Course> courses;



        public CourseRepo()
        {
            courses = new List<Course>
            {
                new Course { Id = 1, Name = "Software Engineering" },
                new Course { Id = 2, Name = "Object-Oriented Programming" },
                new Course { Id = 3, Name = "Parallel Programming" },
                new Course { Id = 4, Name = "Image Processing" },
                new Course { Id = 5, Name = "Mobile Development", }
            };

        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await Task.FromResult(courses);
        }

        public async Task<Course> GetCourseAsync(int id)
        {

            return await Task.FromResult(courses.FirstOrDefault(c => c.Id == id));
        }
    }
}
