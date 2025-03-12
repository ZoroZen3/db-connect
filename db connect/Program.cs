using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace db_connect
{
    class Program
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1.Insert Employee");
                Console.WriteLine("2.Read Employee");
                Console.WriteLine("3.Update Employee");
                Console.WriteLine("4.Delete Employee");
                Console.WriteLine("5.Exit");
                Console.WriteLine("Select a option");

                switch (Console.ReadLine())
                {
                    case "1":
                        InsertEmployee();
                        break;
                    case "2":
                        ReadEmployee();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        Console.WriteLine("Exiting the program");
                        return;
                    default:
                        Console.WriteLine("Enter a valid option");
                        break;
                }
            }
        }

        static void InsertEmployee()
        {
            Console.Write("Enter Name :");
            string name = Console.ReadLine();

            Console.Write("Enter Age :");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Department :");
            string department = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO db_connect (Name, Age, Department) VALUES (@Name, @Age, @Department)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Department", department);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected>0?"Employee inserted successfully" : "Insertion failed");
                }
            }
        }

        static void ReadEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))

            {
                string query = "SELECT * FROM db_connect";
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nID  | Name | Age | Department");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["Age"]} | {reader["Department"]}");
                        }
                    }
                }
            }
        }

        static void UpdateEmployee()
        {
            Console.Write("Enter Employee Id to update : ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter New Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter New Department: ");
            string department = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE db_connect SET Name = @Name , Age = @Age , Department = @Department Where id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Department", department);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected>0 ? "Employee updated successfully" : "Updation failed");
                }
            }
        }

        static void DeleteEmployee()
        {
            Console.WriteLine("Enter Employee ID to delete : ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Delete from db_connect where id = @id";
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected>0?"Employee deleted successfully":"Employee deletion failed");
                }
            }
        }
    }
}