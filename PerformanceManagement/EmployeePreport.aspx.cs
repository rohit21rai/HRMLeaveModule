using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeavePolicy.PerformanceManagement
{
    public partial class EmployeePreport : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

        }

        protected void gvHeatmap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblPerformance");
                double score = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AverageScore"));

                string label = GetLabel(score);
                Color cellColor = GetColor(score);

                lbl.Text = label;
                e.Row.Cells[2].BackColor = cellColor;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            int empId;
            if (int.TryParse(txtEmpID.Text.Trim(), out empId))
            {
                LoadHeatmap(empId);
            }
            else
            {
                lblMsg.Text = "Please enter a valid Employee ID.";
            }
        }

        public void LoadHeatmap(int employeeID)
        {
            
                SqlCommand cmd = new SqlCommand("sp_GetEmployeePerformanceHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvHeatmap.DataSource = dt;
                gvHeatmap.DataBind();

                if (dt.Rows.Count == 0)
                    lblMsg.Text = "No appraisal records found for this employee.";
            
        }

        string GetLabel(double score)
        {
            if (score >= 4.0) return "Excellent";
            else if (score >= 3.0) return "Average";
            else return "Needs Improvement";
        }

        Color GetColor(double score)
        {
            if (score >= 4.0) return Color.LightGreen;
            else if (score >= 3.0) return Color.Khaki;
            else return Color.IndianRed;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

        }
    }
}