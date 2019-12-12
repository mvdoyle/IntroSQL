using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace IntroSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            //var repo = new DepartmentRepository(connString); //Without Dapper

            IDbConnection conn = new MySqlConnection(connString); 
            var repo = new DapperDepartmentRepository(conn);

            //Console.WriteLine("Type a new Department name");

            //var newDepartment = Console.ReadLine();

            //repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }  
        }
    }
}
