using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient; 
using System.Text;

namespace UtilityLibrary
{
    public class QueryBuilder
    {
        private string _tableName;
        private List<string> _columns;
        private List<string> _conditions;
        private List<string> _orderBy;

        public QueryBuilder(string tableName)
        {
            _tableName = tableName;
            _columns = new List<string>();
            _conditions = new List<string>();
            _orderBy = new List<string>();
        }

        public QueryBuilder Select(params string[] columns)
        {
            _columns.AddRange(columns);
            return this;
        }

        public QueryBuilder Where(string condition)
        {
            _conditions.Add(condition);
            return this;
        }

        public QueryBuilder OrderBy(params string[] columns)
        {
            _orderBy.AddRange(columns);
            return this;
        }

        public void ExecuteQuery(string connectionString)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append($"SELECT ");

            if (_columns.Count == 0)
            {
                queryBuilder.Append("*"); 
            }
            else
            {
                queryBuilder.Append(string.Join(", ", _columns));
            }

            queryBuilder.Append($" FROM {_tableName}");

            if (_conditions.Count > 0)
            {
                queryBuilder.Append(" WHERE " + string.Join(" AND ", _conditions));
            }

            if (_orderBy.Count > 0)
            {
                queryBuilder.Append(" ORDER BY " + string.Join(", ", _orderBy));
            }

            string sqlQuery = queryBuilder.ToString();
            Console.WriteLine($"Executing SQL Query: {sqlQuery}"); 

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write($"{reader[i]} ");
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while executing the query: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
