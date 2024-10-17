using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using UtilityLibrary;
using SISLibrary.Entity;
using System.Data;

namespace SISLibrary.DAO.Impl
{
    public class EnrollmentDAOImpl : IEnrollmentDAO
    {
        private StudentDAOImpl studentDAO;
        private CourseDAOImpl courseDAO;

        public List<Enrollments> GetEnrollmentsForStudent(int studentId)
        {
            List<Enrollments> enrollments = new List<Enrollments>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT e.enrollment_id, e.student_id, e.course_id, e.enrollment_date, " +
                                   "s.first_name, s.last_name, s.date_of_birth, s.email, s.phone_number, " +
                                   "c.course_name, c.credits, c.teacher_id " +
                                   "FROM Enrollments e " +
                                   "JOIN Students s ON e.student_id = s.student_id " +
                                   "JOIN Courses c ON e.course_id = c.course_id " +
                                   "WHERE e.student_id = @StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var student = new Students(
                                    reader.GetInt32(1),
                                    reader.GetString(4),
                                    reader.GetString(5),
                                    reader.GetDateTime(6),
                                    reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    reader.IsDBNull(8) ? "" : reader.GetString(8)
                                );

                                var course = new Courses(
                                    reader.GetInt32(2),
                                    reader.GetString(9),
                                    reader.GetInt32(10),
                                    reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11)
                                );

                                enrollments.Add(new Enrollments(
                                    reader.GetInt32(0),
                                    student,
                                    course,
                                    reader.GetDateTime(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving enrollments: {ex.Message}");
            }
            return enrollments;
        }

        public List<Enrollments> GetAllEnrollments()
        {
            List<Enrollments> enrollments = new List<Enrollments>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT e.enrollment_id, e.student_id, e.course_id, e.enrollment_date, " +
                                   "s.first_name, s.last_name, s.date_of_birth, s.email, s.phone_number, " +
                                   "c.course_name, c.credits, c.teacher_id " +
                                   "FROM Enrollments e " +
                                   "JOIN Students s ON e.student_id = s.student_id " +
                                   "JOIN Courses c ON e.course_id = c.course_id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var student = new Students(
                                    reader.GetInt32(1),
                                    reader.GetString(4),
                                    reader.GetString(5),
                                    reader.GetDateTime(6),
                                    reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    reader.IsDBNull(8) ? "" : reader.GetString(8)
                                );

                                var course = new Courses(
                                    reader.GetInt32(2),
                                    reader.GetString(9),
                                    reader.GetInt32(10),
                                    reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11)
                                );

                                enrollments.Add(new Enrollments(
                                    reader.GetInt32(0),
                                    student,
                                    course,
                                    reader.GetDateTime(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving enrollments: {ex.Message}");
            }
            return enrollments;
        }

        public List<Enrollments> GetEnrollmentsForCourse(int courseId)
        {
            List<Enrollments> enrollments = new List<Enrollments>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT e.enrollment_id, e.student_id, e.course_id, e.enrollment_date, " +
                                   "s.first_name, s.last_name " +
                                   "FROM Enrollments e " +
                                   "JOIN Students s ON e.student_id = s.student_id " +
                                   "WHERE e.course_id = @CourseId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseId", courseId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var student = new Students(
                                    reader.GetInt32(1),
                                    reader.GetString(4),
                                    reader.GetString(5),
                                    DateTime.Now,
                                    "",
                                    ""
                                );

                                enrollments.Add(new Enrollments(
                                    reader.GetInt32(0),
                                    student,
                                    null,
                                    reader.GetDateTime(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving enrollments for the course: {ex.Message}");
            }
            return enrollments;
        }

        public List<Enrollments> GetEnrollmentsByCourseName(string courseName)
        {
            List<Enrollments> enrollments = new List<Enrollments>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT e.enrollment_id, e.student_id, e.course_id, e.enrollment_date, " +
                                   "s.first_name, s.last_name, s.date_of_birth, s.email, s.phone_number, " +
                                   "c.course_name, c.credits " +
                                   "FROM Enrollments e " +
                                   "JOIN Students s ON e.student_id = s.student_id " +
                                   "JOIN Courses c ON e.course_id = c.course_id " +
                                   "WHERE c.course_name = @CourseName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", courseName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var student = new Students(
                                    reader.GetInt32(1),
                                    reader.GetString(4),
                                    reader.GetString(5),
                                    reader.GetDateTime(6),
                                    reader.GetString(7),
                                    reader.GetString(8)
                                );

                                var course = new Courses(
                                    reader.GetInt32(2),
                                    reader.GetString(9),
                                    reader.GetInt32(10),
                                    null
                                );

                                enrollments.Add(new Enrollments(
                                    reader.GetInt32(0),
                                    student,
                                    course,
                                    reader.GetDateTime(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving enrollments: {ex.Message}");
            }
            return enrollments;
        }

        public void AddEnrollment(Enrollments enrollment)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();

                    string query = "INSERT INTO Enrollments (student_id, course_id, enrollment_date) " +
                                   "VALUES (@studentId, @courseId, @enrollmentDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", enrollment.Student.StudentID);
                        command.Parameters.AddWithValue("@courseId", enrollment.Course.CourseID);
                        command.Parameters.AddWithValue("@enrollmentDate", enrollment.EnrollmentDate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while adding enrollment: {ex.Message}");
            }
        }

        public Students GetStudentById(int studentId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Students WHERE student_id = @StudentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Students(
                                    reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetDateTime(3),
                                    reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    reader.IsDBNull(5) ? "" : reader.GetString(5)
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the student: {ex.Message}");
            }
            return null;
        }

        public void UpdateEnrollment(Enrollments enrollment)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Enrollments SET course_id = @courseId, enrollment_date = @enrollmentDate " +
                                   "WHERE enrollment_id = @enrollmentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enrollmentId", enrollment.EnrollmentID);
                        command.Parameters.AddWithValue("@courseId", enrollment.Course.CourseID);
                        command.Parameters.AddWithValue("@enrollmentDate", enrollment.EnrollmentDate);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while updating the enrollment: {ex.Message}");
            }
        }

        public void DeleteEnrollment(int enrollmentId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Enrollments WHERE enrollment_id = @enrollmentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enrollmentId", enrollmentId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while deleting the enrollment: {ex.Message}");
            }
        }
    }
}
