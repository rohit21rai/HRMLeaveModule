<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraiseEmployee.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.AppraiseEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Appraise Employee</h2>

    <label>Select Employee:</label><br />
    <asp:DropDownList ID="ddlEmployee" runat="server" /><br /><br />

    <label>Select Template:</label><br />
    <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" /><br /><br />

    <label>Reviewer Name:</label><br />
    <asp:TextBox ID="txtReviewer" runat="server" /><br /><br />

    <asp:GridView ID="gvCriteria" runat="server" AutoGenerateColumns="False" DataKeyNames="CriteriaID">
        <Columns>
            <asp:BoundField DataField="CriteriaName" HeaderText="KPI" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:TemplateField HeaderText="Score (1–5)">
                <ItemTemplate>
                    <asp:TextBox ID="txtScore" runat="server" Width="50px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comment">
                <ItemTemplate>
                    <asp:TextBox ID="txtComment" runat="server" Width="200px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView><br />

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Appraisal" OnClick="btnSubmit_Click" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />

            <asp:Button ID="btnDownloadPDF" runat="server" Text="Download Appraisal as PDF" OnClick="btnDownloadPDF_Click" Visible="false" />


        </div>
    </form>
</body>
</html>
