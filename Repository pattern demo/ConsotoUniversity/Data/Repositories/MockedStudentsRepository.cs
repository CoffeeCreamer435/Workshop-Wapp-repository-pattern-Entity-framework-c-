using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repositories
{
    public class MockedStudentsRepository : IStudentsRepository
    {
        private IEnumerable<Student> _students;

        private IEnumerable<Student> _updatedStudents;
        private int _lastID
        {
            get
            {
                var lastStudent = _updatedStudents.OrderByDescending(student => student.ID).FirstOrDefault();
                return lastStudent != default ? lastStudent.ID : -1;
            }
        }

        public MockedStudentsRepository()
        {
            _students = new List<Student>(new[] {
                new Student { ID = 0, FirstMidName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { ID = 1, FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { ID = 2, FirstMidName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { ID = 3, FirstMidName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { ID = 4, FirstMidName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { ID = 5, FirstMidName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { ID = 6, FirstMidName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { ID = 7, FirstMidName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            }).AsEnumerable(); 

            _updatedStudents = _students;
        }

        public IQueryable<Student> GetStudents()
        {
            return _students.AsQueryable();
        }

        public Student GetStudentByID(int id)
        {
            return _students.FirstOrDefault(student => student.ID == id);
        }

        public void InsertStudent(Student student)
        {
            student.ID = _lastID + 1;
            _updatedStudents = _updatedStudents.Concat(new Student[] { student });
        }

        public void DeleteStudent(int id)
        {
            _updatedStudents = _updatedStudents.Where(student => student.ID != id);
        }

        public void UpdateStudent(Student updatedStudent)
        {

            _updatedStudents = _updatedStudents.Select(student => updatedStudent.ID == student.ID ? updatedStudent : student);
        }

        public void Save()
        {
            _updatedStudents = _students = _updatedStudents;
        }
    }
}
