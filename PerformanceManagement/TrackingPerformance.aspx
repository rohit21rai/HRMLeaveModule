<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackingPerformance.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.TrackingPerformance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Test Employee Performance Data</h2>

    <label>Enter Employee ID:</label>
    <asp:TextBox ID="txtEmpID" runat="server" Width="100px" />
    <asp:Button ID="btnLoad" runat="server" Text="Load Data" OnClick="btnLoad_Click" /><br /><br />

    <asp:GridView ID="gvPerformance" runat="server" AutoGenerateColumns="true" EmptyDataText="No records found." />

    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />

        </div>
    </form>
</body>
</html>
