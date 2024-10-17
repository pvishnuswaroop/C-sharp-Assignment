using System;
using System.Collections.Generic;
using SISLibrary.DAO;
using SISLibrary.DAO.Impl;
using SISLibrary.Entity;

class Program
{
    static void Main()
    {
       
        StudentDAOImpl studentDAO = new StudentDAOImpl();
        CourseDAOImpl courseDAO = new CourseDAOImpl();
        EnrollmentDAOImpl enrollmentDAO = new EnrollmentDAOImpl();
        PaymentDAOImpl paymentDAO = new PaymentDAOImpl();
        TeacherDAOImpl teacherDAO = new TeacherDAOImpl();

        
        Console.WriteLine("Enrolling John Doe...");
        EnrollJohnDoe(studentDAO, courseDAO, enrollmentDAO);

        
        Console.WriteLine("Assigning Teacher...");
        AssignTeacherToCourse(courseDAO, teacherDAO);

        Console.WriteLine("Recording Payment...");
        RecordStudentPayment(studentDAO, paymentDAO);

        
        Console.WriteLine("Generating Enrollment Report...");
        GenerateEnrollmentReport(enrollmentDAO, "Computer Science 101");
    }

    static void EnrollJohnDoe(StudentDAOImpl studentDAO, CourseDAOImpl courseDAO, EnrollmentDAOImpl enrollmentDAO)
    {
        var johnCourses = new List<string> { "Introduction to Programming", "Mathematics 101" };

        
        var john = new Students(0, "John5", "Doe", new DateTime(1995, 8, 15), "john5.doe@example.com", "123-456-7890");

        try
        {
            
            int studentId = studentDAO.AddStudent(john); 
            john.StudentID = studentId;
            Console.WriteLine($"Student {john.FirstName} {john.LastName} added successfully with ID {studentId}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding student: {ex.Message}");
            return; 
        }

        
        foreach (var courseName in johnCourses)
        {
            try
            {
                
                Courses course = courseDAO.GetCourseByName(courseName);
                if (course != null)
                {
                    
                    var enrollment = new Enrollments(0, john, course, DateTime.Now);
                    enrollmentDAO.AddEnrollment(enrollment);
                    Console.WriteLine($"{john.FirstName} {john.LastName} has been enrolled in {course.CourseName}.");
                }
                else
                {
                    Console.WriteLine($"Course '{courseName}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enrolling in {courseName}: {ex.Message}");
            }
        }

        Console.WriteLine("John Doe has been successfully enrolled in selected courses.\n");
    }

    static void AssignTeacherToCourse(CourseDAOImpl courseDAO, TeacherDAOImpl teacherDAO)
    {
        
        var teacher = new Teacher(0, "Sarah", "Smith", "sarah.smith@example.com");

        try
        {
            
            var existingTeacher = teacherDAO.GetTeacherByEmail(teacher.Email);
            if (existingTeacher == null)
            {
                teacherDAO.AddTeacher(teacher);
                Console.WriteLine($"Teacher {teacher.FirstName} {teacher.LastName} added successfully.");
            }
            else
            {
                teacher = existingTeacher; 
                Console.WriteLine($"Teacher {teacher.FirstName} {teacher.LastName} already exists.");
            }

            
            Courses course = courseDAO.GetCourseByName("Advanced Database Management");
            if (course != null)
            {
                
                course.TeacherId = teacher.TeacherID; 
                courseDAO.UpdateCourse(course);
                Console.WriteLine($"Assigned {teacher.FirstName} {teacher.LastName} to {course.CourseName}.");
            }
            else
            {
                Console.WriteLine("Course 'Advanced Database Management' not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error assigning teacher: {ex.Message}");
        }
    }

    static void RecordStudentPayment(StudentDAOImpl studentDAO, PaymentDAOImpl paymentDAO)
    {
        
        var studentId = 66; 
        var amount = 500;
        var paymentDate = new DateTime(2023, 4, 10);

        try
        {
            
            var student = studentDAO.GetStudentById(studentId);
            if (student != null)
            {
                
                var payment = new Payments(0, student, amount, paymentDate);
                paymentDAO.AddPayment(payment);
                Console.WriteLine($"Payment of {amount} recorded for {student.FirstName} {student.LastName} on {paymentDate.ToShortDateString()}.");

                
                studentDAO.UpdateStudentBalance(student, amount); 
                Console.WriteLine($"Outstanding balance updated for {student.FirstName} {student.LastName}.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error recording payment: {ex.Message}");
        }
    }

    static void GenerateEnrollmentReport(EnrollmentDAOImpl enrollmentDAO, string courseName)
    {
        try
        {
            
            List<Enrollments> enrollments = enrollmentDAO.GetEnrollmentsByCourseName(courseName);
            if (enrollments.Count > 0)
            {
                Console.WriteLine($"\nEnrollment Report for {courseName}:");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Enrollment ID | Student Name         | Enrollment Date");

                foreach (var enrollment in enrollments)
                {
                    Console.WriteLine($"{enrollment.EnrollmentID,-15} | {enrollment.Student.FirstName} {enrollment.Student.LastName,-20} | {enrollment.EnrollmentDate.ToShortDateString()}");
                }

                Console.WriteLine("------------------------------------------------");
            }
            else
            {
                Console.WriteLine($"No enrollments found for the course '{courseName}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating enrollment report: {ex.Message}");
        }
    }
}
