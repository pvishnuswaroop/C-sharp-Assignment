using SISLibrary.Entity;
using SISLibrary.DAO;
using SISLibrary.DAO.Impl;
using SISLibrary.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SISLibrary.Service
{
    public class SIS
    {
        private IStudentDAO studentDAO = new StudentDAOImpl();
        private ICourseDAO courseDAO = new CourseDAOImpl();
        private IEnrollmentDAO enrollmentDAO = new EnrollmentDAOImpl();
        private IPaymentDAO paymentDAO = new PaymentDAOImpl();
        private ITeacherDAO teacherDAO = new TeacherDAOImpl();

        public void AddEnrollment(Students student, Courses course, DateTime enrollmentDate)
        {
            if (student == null) throw new StudentNotFoundException("Student not found.");
            if (course == null) throw new CourseNotFoundException("Course not found.");

            if (student.Enrollments == null)
                student.Enrollments = new List<Enrollments>();

            if (course.Enrollments == null)
                course.Enrollments = new List<Enrollments>();

            Enrollments enrollment = new Enrollments(enrollmentDAO.GetAllEnrollments().Count + 1, student, course, enrollmentDate);
            enrollmentDAO.AddEnrollment(enrollment);
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
            Console.WriteLine($"{student.FirstName} has been enrolled in {course.CourseName}.");
        }

        public void AssignCourseToTeacher(Courses course, Teacher teacher)
        {
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            if (course == null) throw new CourseNotFoundException("Course not found.");

            if (teacher.AssignedCourses == null)
                teacher.AssignedCourses = new List<Courses>();

            teacher.AssignedCourses.Add(course);
            Console.WriteLine($"Assigned {teacher.FirstName} to teach {course.CourseName}.");
        }

        public void AddPayment(Students student, decimal amount, DateTime paymentDate)
        {
            if (student == null) throw new StudentNotFoundException("Student not found.");

            Payments payment = new Payments(paymentDAO.GetAllPayments().Count + 1, student, amount, paymentDate);
            paymentDAO.AddPayment(payment);

            if (student.PaymentHistory == null)
                student.PaymentHistory = new List<Payments>();

            student.PaymentHistory.Add(payment);
            Console.WriteLine($"Recorded payment of ₹{amount:F2} for {student.FirstName}.");
        }

        public void GenerateEnrollmentReport(Courses course)
        {
            var enrollments = enrollmentDAO.GetEnrollmentsForCourse(course.CourseID);
            if (enrollments.Count == 0)
            {
                Console.WriteLine($"No students are enrolled in {course.CourseName}.");
                return;
            }

            Console.WriteLine($"Enrollment Report for {course.CourseName}:");
            foreach (var enrollment in enrollments)
            {
                Console.WriteLine($"Student: {enrollment.Student.FirstName} {enrollment.Student.LastName}, Enrollment Date: {enrollment.EnrollmentDate.ToShortDateString()}");
            }
        }

        public void GeneratePaymentReport(Students student)
        {
            if (student == null) throw new StudentNotFoundException("Student not found.");

            var payments = student.PaymentHistory;
            if (payments == null || payments.Count == 0)
            {
                Console.WriteLine($"{student.FirstName} has made no payments.");
                return;
            }

            Console.WriteLine($"Payment Report for {student.FirstName} {student.LastName}:");
            foreach (var payment in payments)
            {
                Console.WriteLine($"Amount: ₹{payment.Amount:F2}, Date: {payment.PaymentDate.ToShortDateString()}");
            }
        }

        public List<Courses> GetCoursesForTeacher(Teacher teacher)
        {
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            return teacher.AssignedCourses ?? new List<Courses>();
        }

        public void CalculateCourseStatistics(Courses course)
        {
            if (course == null) throw new CourseNotFoundException("Course not found.");

            int totalEnrollments = enrollmentDAO.GetEnrollmentsForCourse(course.CourseID).Count;
            decimal totalPayments = 0;

            var enrollments = enrollmentDAO.GetEnrollmentsForCourse(course.CourseID);
            foreach (var enrollment in enrollments)
            {
                var studentPayments = paymentDAO.GetPaymentsForStudent(enrollment.Student.StudentID);
                totalPayments += studentPayments.Sum(p => p.Amount);
            }

            Console.WriteLine($"Course Statistics for {course.CourseName}:");
            Console.WriteLine($"Total Enrollments: {totalEnrollments}");
            Console.WriteLine($"Total Payments: ₹{totalPayments:F2}");
        }
    }
}
