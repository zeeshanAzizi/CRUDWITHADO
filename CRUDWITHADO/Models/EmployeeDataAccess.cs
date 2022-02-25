using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data;

namespace CRUDWITHADO.Models
{
    public class EmployeeDataAccess
    {
        DBConnection Dbconnection;
        public EmployeeDataAccess()
        {
            Dbconnection = new DBConnection();
        }
        public List<Employees> GetEmployees()
        {
            string Sp = "SP_Employee";
            SqlCommand sql = new SqlCommand(Sp,Dbconnection.Connection);

            sql.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "SELECT_JOIN");
            

            if (Dbconnection.Connection.State == System.Data.ConnectionState.Closed)
            {
              
                Dbconnection.Connection.Open();

            }
            SqlDataReader dr = sql.ExecuteReader();
            List<Employees> employees = new List<Employees>();
            while (dr.Read())
            {
                Employees emp = new Employees();
                emp.Id = (int)dr["id"];
                emp.Name = dr["name"].ToString();
                emp.Email = dr["email"].ToString();
                emp.Gender = dr["gender"].ToString();
                emp.Mobile = dr["mobile"].ToString();
                emp.DName = dr["department"].ToString();
                employees.Add(emp);
            }
            Dbconnection.Connection.Close();
            return employees;
        }
    }
}
