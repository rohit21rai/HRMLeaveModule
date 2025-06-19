using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeavePolicy.PerformanceManagement
{
    public partial class TrackingPerformance : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            int empId;
            if (!int.TryParse(txtEmpID.Text.Trim(), out empId))
            {
                lblMsg.Text = "Please enter a valid Employee ID.";
                return;
            }

            LoadPerformanceData(empId);
        }

        public void LoadPerformanceData(int employeeID)
        {
            
                SqlCommand cmd = new SqlCommand("sp_GetEmployeePerformanceHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPerformance.DataSource = dt;
                gvPerformance.DataBind();

                if (dt.Rows.Count == 0)
                    lblMsg.Text = "No performance data found for this employee.";
            
        }

    }
    
}