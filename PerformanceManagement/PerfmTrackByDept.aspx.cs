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
    public partial class PerfmTrackByDept : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                LoadDepartments();
            }

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dept = ddlDepartment.SelectedValue;
            if (!string.IsNullOrEmpty(dept))
                LoadPerformanceByDepartment(dept);
        }


        void LoadPerformanceByDepartment(string department)
        {
           
                SqlCommand cmd = new SqlCommand("sp_GetPerformanceByDepartment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Department", department);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPerformance.DataSource = dt;
                gvPerformance.DataBind();

                lblMessage.Text = dt.Rows.Count == 0 ? "No records found for this department." : "";
            
        }

        void LoadDepartments()
        {
            
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT eDepartment FROM Emp_Data WHERE estatus = 'Active' AND eDepartment IS NOT NULL", conn);
                
                ddlDepartment.DataSource = cmd.ExecuteReader();
                ddlDepartment.DataTextField = "eDepartment";
                ddlDepartment.DataBind();
                

                ddlDepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department --", ""));
            
        }

    }
}