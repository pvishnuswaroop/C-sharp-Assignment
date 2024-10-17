using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using UtilityLibrary;
using SISLibrary.Entity;

namespace SISLibrary.DAO.Impl
{
    public class StudentDAOImpl : IStudentDAO
    {
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
                        command.Parameters.Add(new SqlParameter("@StudentId", studentId));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Students(
                                    studentId,
                                    reader.IsDBNull(1) ? null : reader.GetString(1),
                                    reader.IsDBNull(2) ? null : reader.GetString(2),
                                    reader.IsDBNull(3) ? default : reader.GetDateTime(3),
                                    reader.IsDBNull(4) ? null : reader.GetString(4),
                                    reader.IsDBNull(5) ? null : reader.GetString(5)
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

        public int AddStudent(Students student)
        {
            using (SqlConnection conn = DBConnectionUtil.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Students (first_name, last_name, date_of_birth, email, phone_number) " +
                             "VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber); " +
                             "SELECT CAST(scope_identity() AS int);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                    int newStudentId = Convert.ToInt32(cmd.ExecuteScalar());
                    return newStudentId;
                }
            }
        }

        public List<Students> GetAllStudents()
        {
            List<Students> students = new List<Students>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Students";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new Students(
                                    reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetDateTime(3),
                                    reader.GetString(4),
                                    reader.GetString(5)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving students: {ex.Message}");
            }
            return students;
        }

        public void AddEnrollment(Enrollments enrollment)
        {
            if (enrollment == null)
                throw new ArgumentNullException(nameof(enrollment), "Enrollment cannot be null");

            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Enrollments (student_id, course_id, enrollment_date) " +
                                   "VALUES (@StudentId, @CourseId, @EnrollmentDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@StudentId", enrollment.Student.StudentID));
                        command.Parameters.Add(new SqlParameter("@CourseId", enrollment.Course.CourseID));
                        command.Parameters.Add(new SqlParameter("@EnrollmentDate", enrollment.EnrollmentDate));
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while adding the enrollment: {ex.Message}");
            }
        }

        public void UpdateStudent(Students student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Students SET first_name = @FirstName, last_name = @LastName, " +
                                   "date_of_birth = @DateOfBirth, email = @Email, phone_number = @PhoneNumber " +
                                   "WHERE student_id = @StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@StudentId", student.StudentID));
                        command.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                        command.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                        command.Parameters.Add(new SqlParameter("@DateOfBirth", student.DateOfBirth));
                        command.Parameters.Add(new SqlParameter("@Email", student.Email));
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", student.PhoneNumber));

                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Student updated successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while updating the student: {ex.Message}");
            }
        }

        public void UpdateOutstandingBalance(Students student, decimal amountPaid)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            var existingStudent = GetStudentById(student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.OutstandingBalance += amountPaid;

                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Students SET OutstandingBalance = @NewBalance WHERE StudentID = @StudentId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@NewBalance", existingStudent.OutstandingBalance));
                        command.Parameters.Add(new SqlParameter("@StudentId", existingStudent.StudentID));
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Outstanding balance updated successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        public void UpdateStudentBalance(Students student, decimal amountPaid)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            var existingStudent = GetStudentById(student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.OutstandingBalance -= amountPaid;

                try
                {
                    using (SqlConnection connection = DBConnectionUtil.GetConnection())
                    {
                        connection.Open();
                        string query = "UPDATE Students SET OutstandingBalance = @NewBalance WHERE student_id = @StudentId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add(new SqlParameter("@NewBalance", existingStudent.OutstandingBalance));
                            command.Parameters.Add(new SqlParameter("@StudentId", existingStudent.StudentID));
                            command.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine("Student balance updated successfully.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"An error occurred while updating the balance: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
    }
}
