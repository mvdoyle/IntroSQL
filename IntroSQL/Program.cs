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

            //get the connection string value from the JSON key value pair
            var connString = config.GetConnectionString("DefaultConnection");


            //-------------------------------IoC-------------------------------
            //container to implement IoC
            var container = new Container(x =>
            {
                x.AddTransient<IDbConnection>((c) =>
                {
                    return new MySqlConnection(connString);
                });

                x.AddTransient<IDepartmentRepository, DapperDepartmentRepository>();

            });

            //var repo = container.GetService<IDepartmentRepository>();
            /*-------------------------------IoC Ends----------------------------*/


            //-------------------------------Without Dapper------------------------
            //
            // IDbConnection conn = new MySqlConnection(connString);
            // var repo = new DepartmentRepository(conn);
            //
            // ----------------------------Without Dapper Ends-------------------*/


            //-------------------------------Dapper--------------------------------
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);
            //-----------------------------Dapper Ends-----------------------------


            //Console.WriteLine("Type a new Department name");

            //var newDepartment = Console.ReadLine();

            //repo.InsertDepartment(newDepartment); 

            var departments = repo.GetAllDepartments();

            Console.WriteLine("ID -- NAME \n---------------------------");
            foreach(var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID}  -- {dept.Name}");
            }  
        }
    }
}
