using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using UtilityLibrary;
using SISLibrary.Entity;
using System.Data;

namespace SISLibrary.DAO.Impl
{
    public class PaymentDAOImpl : IPaymentDAO
    {
        public List<Payments> GetPaymentsForStudent(int studentId)
        {
            List<Payments> payments = new List<Payments>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    string query = "SELECT p.payment_id, p.student_id, p.amount, p.payment_date, " +
                                   "s.first_name, s.last_name " +
                                   "FROM Payments p " +
                                   "JOIN Students s ON p.student_id = s.student_id " +
                                   "WHERE p.student_id = @StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId });
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

                                payments.Add(new Payments(
                                    reader.GetInt32(0),
                                    student,
                                    reader.GetDecimal(2),
                                    reader.GetDateTime(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving payments: {ex.Message}");
            }
            return payments;
        }

        public List<Payments> GetAllPayments()
        {
            List<Payments> payments = new List<Payments>();
            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    string query = "SELECT p.payment_id, p.student_id, p.amount, p.payment_date, " +
                                   "s.first_name, s.last_name " +
                                   "FROM Payments p " +
                                   "JOIN Students s ON p.student_id = s.student_id";

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
                                    DateTime.Now,
                                    "",
                                    ""
                                );

                                payments.Add(new Payments(
                                    reader.GetInt32(0),
                                    student,
                                    reader.GetDecimal(2),
                                    reader.GetDateTime(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving all payments: {ex.Message}");
            }
            return payments;
        }

        public void AddPayment(Payments payment)
        {
            if (payment.Student == null)
            {
                Console.WriteLine("Invalid payment data.");
                return;
            }

            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Payments (student_id, amount, payment_date) " +
                                   "VALUES (@StudentId, @Amount, @PaymentDate)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", payment.Student.StudentID);
                        command.Parameters.AddWithValue("@Amount", payment.Amount);
                        command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Payment added successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while adding the payment: {ex.Message}");
            }
        }

        public void UpdatePayment(Payments payment)
        {
            if (payment.PaymentID <= 0)
            {
                Console.WriteLine("Invalid payment ID.");
                return;
            }

            try
            {
                using (SqlConnection connection = DBConnectionUtil.GetConnection())
                {
                    string query = "UPDATE Payments SET amount = @Amount, payment_date = @PaymentDate " +
                                   "WHERE payment_id = @PaymentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentId", payment.PaymentID);
                        command.Parameters.AddWithValue("@Amount", payment.Amount);
                        command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Payment updated successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while updating the payment: {ex.Message}");
            }
        }
    }
}
