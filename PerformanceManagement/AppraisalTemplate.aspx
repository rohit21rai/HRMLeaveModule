<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraisalTemplate.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.AppraisalTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Add Appraisal Template</h2>

    <label>Template Name:</label><br />
    <asp:TextBox ID="txtTemplateName" runat="server" /><br /><br />

    <label>Review Period:</label><br />
    <asp:TextBox ID="txtPeriod" runat="server" Placeholder="e.g., Q1 2025" /><br /><br />

    <label>Status:</label><br />
    <asp:DropDownList ID="ddlStatus" runat="server">
        <asp:ListItem Text="Active" Value="Active" />
        <asp:ListItem Text="Inactive" Value="Inactive" />
    </asp:DropDownList><br /><br />

    <asp:Button ID="btnAddTemplate" runat="server" Text="Add Template" OnClick="btnAddTemplate_Click" />
    <asp:Label ID="lblTemplateMsg" runat="server" ForeColor="Green" /><br /><br />

    <hr />

    <h2>Add Criteria to Template</h2>

    <label>Select Template:</label><br />
    <asp:DropDownList ID="ddlTemplates" runat="server" /><br /><br />

    <label>Criteria Name:</label><br />
    <asp:TextBox ID="txtCriteriaName" runat="server" /><br /><br />

    <label>Description:</label><br />
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" /><br /><br />

    <label>Status:</label><br />
    <asp:DropDownList ID="ddlCriteriaStatus" runat="server">
        <asp:ListItem Text="Active" Value="Active" />
        <asp:ListItem Text="Inactive" Value="Inactive" />
    </asp:DropDownList><br /><br />

    <asp:Button ID="btnAddCriteria" runat="server" Text="Add Criteria" OnClick="btnAddCriteria_Click" />
    <asp:Label ID="lblCriteriaMsg" runat="server" ForeColor="Green" />

        </div>
    </form>
</body>
</html>
