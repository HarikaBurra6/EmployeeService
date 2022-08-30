using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace DB.Models
{
    public class Repofactory
    {
        private static readonly object padlock = new object();
        static Repofactory repofactory;
        static IRepository Repository;
        private static string Repo = System.Configuration.ConfigurationManager.AppSettings["Repository"].ToString();
        public Repofactory()
        {
            if (Repo == "DB")
            {
                Repository = new DBRepo();
            }
            else
            {
                Repository = new InMemRepo();
            }

        }
        public IRepository GetRepo()
        {
            return Repository;
        }

        public static Repofactory GetInstance()
        {
            if (repofactory == null)
            {
                lock (padlock)
                {
                    if (repofactory == null)
                    {
                        repofactory= new Repofactory();
                    }
                }
            }
            return repofactory;
        }
    }
}