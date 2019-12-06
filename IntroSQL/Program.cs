using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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

            var repo = new DepartmentRepository(connString);

            var departments = repo.GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }

            Console.WriteLine("Would you like to do?");
            var response = "";

            do
            {
                Console.WriteLine("Please type one of the following choices:");
                Console.Write("||Insert | Update | Delete || Departments");
                response = Console.ReadLine();

            } while (response.ToUpper() != "INSERT" || response.ToUpper() != "UPDATE" || response.ToUpper() != "DELETE");

            if(response == "INSERT")
            {

            }
        }
    }
}
