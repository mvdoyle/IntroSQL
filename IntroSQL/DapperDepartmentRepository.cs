using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace IntroSQL
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            using (var conn = _connection)
            {
                return conn.Query<Department>("SELECT * FROM Departments;").ToList();
            }
        }

        public void InsertDepartment(string departmentName)
        {
            using (var conn = _connection)
            {
                conn.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);", new { departmentName = departmentName});
            }
        }
    }
}
