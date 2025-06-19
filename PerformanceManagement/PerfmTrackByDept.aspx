<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfmTrackByDept.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.PerfmTrackByDept" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Track Performance by Department</h2>
            <label>Select Department:</label><br />
<asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" /><br /><br />

            <asp:GridView ID="gvPerformance" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found.">
    <Columns>
        <asp:BoundField DataField="TemplateName" HeaderText="Appraisal Template" />
        <asp:BoundField DataField="ReviewPeriod" HeaderText="Review Period" />
        <asp:BoundField DataField="ReviewerName" HeaderText="Reviewer" />
        <asp:BoundField DataField="ReviewDate" HeaderText="Review Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="AverageScore" HeaderText="Avg. Score" />
    </Columns>
</asp:GridView>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
