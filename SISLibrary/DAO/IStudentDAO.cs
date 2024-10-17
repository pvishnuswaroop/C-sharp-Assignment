using SISLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;


namespace SISLibrary.DAO
{
    public interface IStudentDAO
    {
        int AddStudent(Students student);

        Students GetStudentById(int studentId);
        List<Students> GetAllStudents();
        void UpdateStudent(Students student);

        void UpdateStudentBalance(Students student, decimal amountPaid);
        void UpdateOutstandingBalance(Students student, decimal amountPaid);
        void AddEnrollment(Enrollments enrollment);
    }
}


