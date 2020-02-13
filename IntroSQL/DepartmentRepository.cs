using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace IntroSQL
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Departments;";

                MySqlDataReader reader = cmd.ExecuteReader();

                List<Department> allDepartments = new List<Department>();

                while (reader.Read() == true)
                {
                    var currentDepartment = new Department();
                    currentDepartment.DepartmentID = (int)reader["DepartmentID"];
                    currentDepartment.Name = (string)reader["Name"];

                    allDepartments.Add(currentDepartment);
                }

                return allDepartments;
            }
        }

        public Department GetDepartment(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Departments WhERE DepartmentID = @id;";
                cmd.Parameters.AddWithValue("id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                var department = new Department();

                while (reader.Read() == true)
                {
                    department.DepartmentID = (int)reader["DepartmentID"];
                    department.Name = (string)reader["Name"];
                }

                return department;
            }
        }

        public void InsertDepartment(string departmentName)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);";
                cmd.Parameters.AddWithValue("departmentName", departmentName);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
