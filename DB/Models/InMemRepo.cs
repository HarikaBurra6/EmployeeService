using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB.Models
{
    public class InMemRepo:Employee,IRepository
    {
        static Dictionary<int, Employee> emp = new Dictionary<int, Employee>();
       
        public string Insert(Employee Emp)
        {
            if (Emp.EmployeeID > 0)
            {
                emp.Add(Emp.EmployeeID, Emp);
            }
            if (emp.Count > 0)
            {
                return "Employees added the list";
            }
            else
            {
                return string.Empty;
            }
        }

        public string Get()
        {
            //Employee e = emp[EmployeeID];
            //Employee GetEmp = new Employee();
            //GetEmp.EmployeeID = e.EmployeeID;
            //GetEmp.FirstName = e.FirstName;
            //GetEmp.LastName = e.LastName;
            //GetEmp.Gender = e.Gender;
            //GetEmp.Email = e.Email;
            string JSONresult = JsonConvert.SerializeObject(emp, Formatting.Indented);
            return JSONresult;
        }
    }
}