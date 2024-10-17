using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using UtilityLibrary;
using SISLibrary.Entity;

namespace SISLibrary.DAO.Impl
{
    public class CourseDAOImpl : ICourseDAO
    {
        
        
        public Courses GetCourseById(int courseId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    string query = "SELECT * FROM Courses WHERE course_id = @CourseId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseId", courseId);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Courses(
                                    courseId,
                                    reader["course_name"].ToString(),
                                    reader.GetInt32(2),
                                    reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the course: {ex.Message}");
            }
            return null;
        }

        


        
        public Courses GetCourseByName(string courseName)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 
                    string query = "SELECT * FROM Courses WHERE course_name = @CourseName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", courseName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Courses(
                                    reader.GetInt32(0), 
                                    reader.GetString(1), 
                                    reader.GetInt32(2), 
                                    reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3) 
                                );
                            }
                        }
                    }
                    connection.Close(); 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return null; 
        }

        
        public void AddCourse(Courses course)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 
                    string query = "INSERT INTO Courses (course_name, credits, teacher_id) " +
                                   "VALUES (@CourseName, @Credits, @TeacherId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", course.CourseName);
                        command.Parameters.AddWithValue("@Credits", course.Credits); 
                        command.Parameters.AddWithValue("@TeacherId", (object)course.TeacherId ?? DBNull.Value); 

                        command.ExecuteNonQuery();
                    }
                    connection.Close(); 
                }
                Console.WriteLine("Course added successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while adding the course: {ex.Message}");
            }
        }

        
        public void UpdateCourse(Courses course)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 
                    string query = "UPDATE Courses SET course_name = @CourseName, credits = @Credits, " +
                                   "teacher_id = @TeacherId WHERE course_id = @CourseId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseId", course.CourseID);
                        command.Parameters.AddWithValue("@CourseName", course.CourseName);
                        command.Parameters.AddWithValue("@Credits", course.Credits); 
                        command.Parameters.AddWithValue("@TeacherId", (object)course.TeacherId ?? DBNull.Value); 

                        command.ExecuteNonQuery();
                    }
                    connection.Close(); 
                }
                Console.WriteLine("Course updated successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while updating the course: {ex.Message}");
            }
        }

        
        public List<Courses> GetAllCourses()
        {
            List<Courses> courses = new List<Courses>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 
                    string query = "SELECT course_id, course_name, credits, teacher_id FROM Courses";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int courseId = reader.GetInt32(0);
                                string courseName = reader.GetString(1);
                                int credits = reader.GetInt32(2);
                                int? teacherId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);

                                Courses course = new Courses(courseId, courseName, credits, teacherId);
                                courses.Add(course);
                            }
                        }
                    }
                    connection.Close(); 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving courses: {ex.Message}");
            }
            return courses;
        }

        
        public void AssignTeacher(int courseId, int teacherId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 
                    string query = "UPDATE Courses SET teacher_id = @TeacherId WHERE course_id = @CourseId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseId", courseId);
                        command.Parameters.AddWithValue("@TeacherId", teacherId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Teacher assigned successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No course found with the specified ID.");
                        }
                    }
                    connection.Close(); 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while assigning the teacher: {ex.Message}");
            }
        }

        
        public void DeleteCourse(int courseId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 
                    string query = "DELETE FROM Courses WHERE course_id = @CourseId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseId", courseId);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                Console.WriteLine("Course deleted successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while deleting the course: {ex.Message}");
            }
        }
    }
}
