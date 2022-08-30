using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName
        {
            get; set;
        }
        public string Gender
        {
            get; set;
        }
        public string Email { get; set; }
    }

    public interface IRepository
    {
        int EmployeeID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Gender { get; set; }

        string Insert(Employee Employee);

        string Get();
    }
}