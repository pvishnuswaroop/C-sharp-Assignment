using SISLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SISLibrary.DAO
{
    public interface IEnrollmentDAO
    {
        List<Enrollments> GetEnrollmentsForStudent(int studentId);

        List<Enrollments> GetAllEnrollments();

        void AddEnrollment(Enrollments enrollment);
        void UpdateEnrollment(Enrollments enrollment);

        
        List<Enrollments> GetEnrollmentsForCourse(int courseId);
    }
}

