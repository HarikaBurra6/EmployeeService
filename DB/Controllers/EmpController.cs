using DB.Models;
//using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace DB.Controllers
{
    [Microsoft.AspNetCore.Mvc.Produces("application/json")]
    public class EmpController : Controller
    {

        Repofactory repofactory = Repofactory.GetInstance();
        //Constructor
       
        // GET: Emp
        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }

        public string InsertEmp(Employee Employee)
        {
            IRepository Emp = repofactory.GetRepo();
             string result=Emp.Insert(Employee);
             return result;
        }
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Produces("application/json")]
        public string GetEmp()
        {
            IRepository Emp = repofactory.GetRepo();
            string json=Emp.Get();
            return json;
        }
 
    }

}