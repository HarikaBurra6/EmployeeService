using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DB.Models
{
    public class DBRepo:Employee,IRepository
    {
        private MySqlConnection connection;
        private string server;
        private int Port;
        private string database;
        private string uid;
        private string password;
        //Initialize values

        public DBRepo()
        {
            Initialize();
        }
        public void Initialize()
        {
            server = "localhost";
            Port = 3306;
            database = "TEST";
            uid = "root";
            password = "India@123";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "PORT=" + Port + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection= new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {

                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        Console.ReadLine();
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        Console.ReadLine();
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return false;
            }
        }

        public string Insert(Employee Employee)
        {
            string query = "INSERT INTO employees(EmployeeID,FirstName, LastName,Gender,Email) " +
                   "VALUES (@EmployeeID,@FirstName,@LastName,@Gender,@Email) ";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                // define parameters and their values
                cmd.Parameters.Add("@EmployeeID", MySqlDbType.Int32, 50).Value = Employee.EmployeeID;
                cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar, 50).Value = Employee.FirstName;
                cmd.Parameters.Add("@LastName", MySqlDbType.VarChar, 50).Value = Employee.LastName;
                cmd.Parameters.Add("@Gender", MySqlDbType.VarChar, 50).Value = Employee.Gender;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar, 50).Value = Employee.Email;

                // open connection, execute INSERT, close connection
               
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return "Employee added to the database";
            }
        }
        [Produces("application/json")]
        public string Get()
        {
            string query = "SELECT * FROM employees";
            string JSONresult = null;
            //Create a list to store the result
            List<DBRepo> Employees = new List<DBRepo>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DBRepo Employee = new DBRepo();
                    Employee.EmployeeID = (Int32)dataReader["EmployeeId"];
                    Employee.FirstName = dataReader["FirstName"].ToString();
                    Employee.LastName = dataReader["LastName"].ToString();
                    Employee.Gender = dataReader["Gender"].ToString();
                    Employee.Email = dataReader["Email"].ToString();
                    Employees.Add(Employee);
                }
                JSONresult = JsonConvert.SerializeObject(Employees, Formatting.Indented);

                HttpContext.Current.Response.AppendHeader("Content-Type", "application/json");
                //var table = new DataTable("EmpQuery");
                //table.Load(dataReader);
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return JSONresult;

            }
            return null;
        }
    }

   
}