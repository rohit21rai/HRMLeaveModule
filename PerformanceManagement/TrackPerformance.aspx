<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackPerformance.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.TrackPerformance" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <%--<div>
            
            <asp:Chart ID="PerformanceChart" runat="server" Width="600px" Height="400px" Visible="false">
    <Series>
        <asp:Series Name="Scores" ChartType="Line" XValueMember="ReviewPeriod" YValueMembers="AverageScore" />
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1" />
    </ChartAreas>
</asp:Chart>

        </div>--%>

        <div>
            <h2>Track Employee Performance</h2>

    <label>Search Employee by Name / ID:</label><br />
    <asp:TextBox ID="txtSearch" runat="server" Width="200px" /><br /><br />

    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br /><br />

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
