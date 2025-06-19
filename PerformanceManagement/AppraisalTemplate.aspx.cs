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
    public partial class AppraisalTemplate : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                LoadTemplates();
            }

        }

        public void LoadTemplates()
        {
            string getTemp = "Exec sp_GetAppraisalTemplates";
            SqlCommand cmd = new SqlCommand(getTemp, conn);
            //conn.Open();
            ddlTemplates.DataSource = cmd.ExecuteReader();
            ddlTemplates.DataTextField = "TemplateName";
            ddlTemplates.DataValueField = "TemplateID";
            ddlTemplates.DataBind();
            
        }



        protected void btnAddTemplate_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand("sp_AddAppraisalTemplate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TemplateName", txtTemplateName.Text.Trim());
            cmd.Parameters.AddWithValue("@ReviewPeriod", txtPeriod.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            cmd.ExecuteNonQuery();
            

            lblTemplateMsg.Text = "Appraisal Template added successfully!";
            LoadTemplates(); 
        }

        protected void btnAddCriteria_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand("sp_AddAppraisalCriteria", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TemplateID", ddlTemplates.SelectedValue);
            cmd.Parameters.AddWithValue("@CriteriaName", txtCriteriaName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", ddlCriteriaStatus.SelectedValue);

           
            cmd.ExecuteNonQuery();

            lblCriteriaMsg.Text = "Criteria added to the selected template!";
        }
    }
}