using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using UtilityLibrary;
using SISLibrary.Entity;

namespace SISLibrary.DAO.Impl
{
    public class TeacherDAOImpl : ITeacherDAO
    {
        public Teacher GetTeacherById(int teacherId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();  
                    string query = "SELECT * FROM Teacher WHERE teacher_id = @TeacherId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TeacherId", teacherId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Teacher(
                                    reader.GetInt32(0), 
                                    reader.GetString(1), 
                                    reader.GetString(2), 
                                    reader.GetString(3)  
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the teacher: {ex.Message}");
            }
            return null; 
        }


        public void AssignTeacherToCourse(int teacherId, int courseId)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Courses SET teacher_id = @TeacherId WHERE course_id = @CourseId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TeacherId", teacherId);
                        command.Parameters.AddWithValue("@CourseId", courseId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Teacher assigned to course successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No course was found to assign the teacher.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while assigning the teacher to the course: {ex.Message}");
            }
        }

        
        public void AddTeacher(Teacher teacher)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open(); 

                    
                    string query = "INSERT INTO Teachers (first_name, last_name, email) OUTPUT INSERTED.teacher_id VALUES (@FirstName, @LastName, @Email)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                        command.Parameters.AddWithValue("@LastName", teacher.LastName);
                        command.Parameters.AddWithValue("@Email", teacher.Email);

                        
                        object result = command.ExecuteScalar();
                        if (result != null) 
                        {
                            teacher.TeacherID = (int)result; 
                        }
                    }
                }
                Console.WriteLine("Teacher added successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while adding the teacher: {ex.Message}");
            }
        }


        public Teacher GetTeacherByEmail(string email)
        {
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();  

                    string query = "SELECT * FROM Teacher WHERE email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Teacher(
                                    reader.GetInt32(reader.GetOrdinal("teacher_id")), 
                                    reader.GetString(reader.GetOrdinal("first_name")), 
                                    reader.GetString(reader.GetOrdinal("last_name")),  
                                    reader.GetString(reader.GetOrdinal("email"))    
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the teacher by email: {ex.Message}");
            }
            return null; 
        }


        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    string query = "SELECT * FROM Teacher";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                teachers.Add(new Teacher(
                                    reader.GetInt32(0), 
                                    reader.GetString(1), 
                                    reader.GetString(2), 
                                    reader.GetString(3)  
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving teachers: {ex.Message}");
            }
            return teachers; 
        }
    }
}
