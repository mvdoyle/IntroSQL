using System;
using System.Data;
using System.IO;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            var connString = config.GetConnectionString("DefaultConnection");

            var container = new Container(x =>
            {
                x.AddTransient<IDbConnection>((c) =>
                {
                    return new MySqlConnection(connString);
                });

                x.AddTransient<IDepartmentRepository, DapperDepartmentRepository>();

            });

            var repo = container.GetService<IDepartmentRepository>();
            

            //var repo = new DepartmentRepository(connString); //Without Dapper

            //var repo = new DapperDepartmentRepository(conn);

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
