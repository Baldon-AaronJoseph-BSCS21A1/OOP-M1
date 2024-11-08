using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ICollection<Student> _students;

        public StudentRepository()
        {
            _students = new List<Student>();
        }

        public void Add(string firstName, string lastName, string section, string course)
        {
            _students.Add(new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Section = section,
                Course = course,
                ID = _students.Count + 1
            });
        }

        public IEnumerable<Student> Get() => _students.ToList();

        public void Update(int id, string firstName, string lastName, string section, string course)
        {
            var student = _students.FirstOrDefault(s => s.ID == id);
            if (student != null)
            {
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Section = section;
                student.Course = course;
            }
        }
    }
}