using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using MVC1.Models;
using System.Data.SqlClient;

namespace MVC1.Controllers
{
    public class EmployeeController : Controller
    {
        SqlConnection con = new SqlConnection("data source=DESKTOP-OK4SOHA\\SQLEXPRESS;initial catalog=MVC;integrated security=true");
        public ActionResult EmployeeForm()
        {
            return View();
        }

      
      
        public JsonResult EmployeeGridShow()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Proc_Select", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);



            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data,JsonRequestBehavior.AllowGet);

        }

        public void EmployeeInsertUpdate(EmpModel obj)
        {
            if(obj.Idd == 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Proc_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@city", obj.City);
                cmd.Parameters.AddWithValue("@salary", obj.Salary);
                cmd.ExecuteNonQuery();

                con.Close();

            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Proc_EmpUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", obj.Idd);
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@Salary", obj.Salary);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public void EmployeeDelete(EmpModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Proc_EmpDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id",obj.Idd);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public JsonResult EmployeeEdit(EmpModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Proc_EmpEdit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id",obj.Idd);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet); ;
        }


    }
}