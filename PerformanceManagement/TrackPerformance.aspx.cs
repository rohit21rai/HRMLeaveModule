using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeavePolicy.PerformanceManagement
{
    public partial class TrackPerformance : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(search))
            {
                lblMessage.Text = "Please enter an Employee ID or Name.";
                return;
            }

            int empId;
            if (int.TryParse(search, out empId))
            {
                LoadPerformanceHistory(empId);
            }
            else
            {
                empId = GetEmployeeIdByName(search);
                if (empId > 0)
                    LoadPerformanceHistory(empId);
                else
                    lblMessage.Text = "Employee not found.";
            }
        }

        public void LoadPerformanceHistory(int employeeID)
        {
            lblMessage.Text = "";
           
                SqlCommand cmd = new SqlCommand("sp_GetEmployeePerformanceHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPerformance.DataSource = dt;
                gvPerformance.DataBind();

                if (dt.Rows.Count == 0)
                    lblMessage.Text = "No performance records found.";
            
        }

        int GetEmployeeIdByName(string name)
        {
            int id = -1;
            
                SqlCommand cmd = new SqlCommand("sp_GetEmployeeIdByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
               
                object result = cmd.ExecuteScalar();
                if (result != null)
                    id = Convert.ToInt32(result);
                
            
            return id;
        }

    }
}