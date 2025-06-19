using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeavePolicy.PerformanceManagement
{
    public partial class AppraiseEmployee : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["LeaveCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                LoadEmployees();
                LoadTemplates();
            }

        }

        public void LoadEmployees()
        {
            string GetAllEmp = "Exec sp_GetAllEmployees";
            SqlCommand cmd = new SqlCommand(GetAllEmp, conn);
            ddlEmployee.DataSource = cmd.ExecuteReader();
            ddlEmployee.DataTextField = "eName";
            ddlEmployee.DataValueField = "eid";
            ddlEmployee.DataBind();
            conn.Close();
        }

        public void LoadTemplates()
        {
            string getApTemp = "Exec sp_GetAppraisalTemplates";
            SqlCommand cmd = new SqlCommand(getApTemp, conn);
            conn.Open();
            ddlTemplate.DataSource = cmd.ExecuteReader();
            ddlTemplate.DataTextField = "TemplateName";
            ddlTemplate.DataValueField = "TemplateID";
            ddlTemplate.DataBind();
            //conn.Close();
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int employeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
            int templateID = Convert.ToInt32(ddlTemplate.SelectedValue);
            string reviewer = txtReviewer.Text.Trim();
            int appraisalID = 0;

                SqlCommand cmd = new SqlCommand("sp_SaveEmployeeAppraisal", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@TemplateID", templateID);
                cmd.Parameters.AddWithValue("@ReviewerName", reviewer);
                SqlParameter output = new SqlParameter("@AppraisalID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(output);
                cmd.ExecuteNonQuery();
                appraisalID = (int)output.Value;

                // Loop through GridView and insert ratings
                foreach (GridViewRow row in gvCriteria.Rows)
                {
                    int criteriaID = Convert.ToInt32(gvCriteria.DataKeys[row.RowIndex].Value);
                    int score = int.Parse(((TextBox)row.FindControl("txtScore")).Text);
                    string comment = ((TextBox)row.FindControl("txtComment")).Text;

                    SqlCommand rateCmd = new SqlCommand("sp_SaveAppraisalRating", conn);
                    rateCmd.CommandType = CommandType.StoredProcedure;
                    rateCmd.Parameters.AddWithValue("@AppraisalID", appraisalID);
                    rateCmd.Parameters.AddWithValue("@CriteriaID", criteriaID);
                    rateCmd.Parameters.AddWithValue("@Score", score);
                    rateCmd.Parameters.AddWithValue("@Comment", comment);
                    rateCmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Appraisal submitted successfully.";
            btnDownloadPDF.Visible = true;

        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {

            int TempID = int.Parse(ddlTemplate.SelectedValue);
            string getCrtTemp = $"Exec sp_GetCriteriaByTemplate '{TempID}'";
            SqlCommand cmd = new SqlCommand(getCrtTemp, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvCriteria.DataSource = dt;
            gvCriteria.DataBind();
        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            int employeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
            int templateID = Convert.ToInt32(ddlTemplate.SelectedValue);
            string reviewer = txtReviewer.Text;

            // Fetch data again
            DataTable dt = new DataTable();
            
                SqlCommand cmd = new SqlCommand("sp_GetCriteriaByTemplate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TemplateID", templateID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            

            // Start PDF generation
            Document pdfDoc = new Document(PageSize.A4);
            MemoryStream ms = new MemoryStream();
            PdfWriter.GetInstance(pdfDoc, ms).CloseStream = false;
            pdfDoc.Open();

            pdfDoc.Add(new Paragraph("Employee Appraisal Form"));
            pdfDoc.Add(new Paragraph("Employee ID: " + employeeID));
            pdfDoc.Add(new Paragraph("Reviewer: " + reviewer));
            pdfDoc.Add(new Paragraph("Review Date: " + DateTime.Now.ToShortDateString()));
            pdfDoc.Add(new Paragraph(" ")); // Spacer

            // Create a table
            PdfPTable table = new PdfPTable(3);
            table.AddCell("KPI");
            table.AddCell("Score");
            table.AddCell("Comment");

            foreach (GridViewRow row in gvCriteria.Rows)
            {
                string kpi = row.Cells[0].Text; // directly from BoundField
                string score = ((TextBox)row.FindControl("txtScore")).Text;
                string comment = ((TextBox)row.FindControl("txtComment")).Text;

                table.AddCell(kpi);
                table.AddCell(score);
                table.AddCell(comment);
            }

            pdfDoc.Add(table);
            pdfDoc.Close();

            byte[] bytes = ms.ToArray();
            ms.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=AppraisalForm.pdf");
            Response.BinaryWrite(bytes);
            Response.End();
        }
    }
}