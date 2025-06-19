using Org.BouncyCastle.Ocsp;
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
    public partial class EmployeePerformanceDashboard : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

        }

          
        public string GetBarStyle(object scoreObj)
        {
            double score = Convert.ToDouble(scoreObj);
            int percent = (int)((score / 5.0) * 100);
            string color;

            if (score >= 4.0)
                color = "#28a745"; // Green
            else if (score >= 3.0)
                color = "#ffc107"; // Yellow
            else
                color = "#dc3545"; // Red

            return $"width:{percent}%; background-color:{color};";
        }

        public string GetDialStyle(object scoreObj)
        {
            double score = Convert.ToDouble(scoreObj);
            string color;

            if (score >= 4.0)
                color = "#28a745"; // Green
            else if (score >= 3.0)
                color = "#ffc107"; // Yellow
            else
                color = "#dc3545"; // Red

            return $"border-color:{color};";
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int empId;
            if (!int.TryParse(txtEmpID.Text.Trim(), out empId))
            {
                lblMsg.Text = "Please enter a valid Employee ID.";
                return;
            }

            DataTable dt = FetchPerformanceData(empId);

            if (dt.Rows.Count == 0)
            {
                lblMsg.Text = "No performance data found for this employee.";
                return;
            }

            // Bind to all visual components
            gvHeatmap.DataSource = dt;
            gvHeatmap.DataBind();

            rptProgress.DataSource = dt;
            rptProgress.DataBind();

            rptDials.DataSource = dt;
            rptDials.DataBind();

            GenerateChartScript(dt);
        }


        public DataTable FetchPerformanceData(int empId)
        {
            DataTable dt = new DataTable();
            
                SqlCommand cmd = new SqlCommand("sp_GetEmployeePerformanceHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", empId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            
            return dt;
        }

        public void GenerateChartScript(DataTable dt)
        {
            StringBuilder labels = new StringBuilder();
            StringBuilder scores = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                labels.Append("'" + row["ReviewPeriod"].ToString() + "',");
                scores.Append(row["AverageScore"].ToString() + ",");
            }

            if (labels.Length == 0 || scores.Length == 0)
            {
                litChartScript.Text = "<script>alert('No chart data.');</script>";
                return;
            }

            string labelStr = labels.ToString().TrimEnd(',');
            string scoreStr = scores.ToString().TrimEnd(',');

            litChartScript.Text = $@"
<script>
var ctx = document.getElementById('scoreChart').getContext('2d');
new Chart(ctx, {{
    type: 'bar',
    data: {{
        labels: [{labelStr}],
        datasets: [{{
            label: 'Average Score',
            data: [{scoreStr}],
            backgroundColor: 'rgba(54, 162, 235, 0.6)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }}]
    }},
    options: {{
        scales: {{
            y: {{
                beginAtZero: true,
                max: 5
            }}
        }}
    }}
}});
</script>";
        }

        protected void gvHeatmap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblPerformance");
                double score = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AverageScore"));

                if (score >= 4.0)
                {
                    lbl.Text = "Excellent";
                    e.Row.Cells[2].BackColor = System.Drawing.Color.LightGreen;
                }
                else if (score >= 3.0)
                {
                    lbl.Text = "Average";
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Khaki;
                }
                else
                {
                    lbl.Text = "Needs Improvement";
                    e.Row.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                }
            }
        }
    }
}