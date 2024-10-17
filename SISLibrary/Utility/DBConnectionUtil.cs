using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;

namespace UtilityLibrary
{
    public static class DBConnectionUtil
    {
        private static IConfigurationRoot Configuration { get; set; }

        static DBConnectionUtil()
        {
            try
            {
                
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Failed to load configuration: " + ex.Message);
                throw; 
            }
        }

        public static SqlConnection GetConnection()
        {
            string connectionString = Configuration.GetConnectionString("SISDBConnection");
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create SQL connection: {ex.Message}");
                throw; 
            }
        }

        public static void TestConnection()
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Connection failed: {ex.Message}");
                }
            }
        }
    }
}
