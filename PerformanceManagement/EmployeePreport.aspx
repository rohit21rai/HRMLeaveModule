<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePreport.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.EmployeePreport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .progress-container {
            width: 400px;
            background-color: #f1f1f1;
            border-radius: 5px;
            overflow: hidden;
        }
        .progress-bar {
            height: 24px;
            color: white;
            text-align: center;
            line-height: 24px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Performance Heatmap (Color-Coded Scores)</h2>

    <label>Enter Employee ID:</label>
    <asp:TextBox ID="txtEmpID" runat="server" Width="150px" />
    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" /><br /><br />

    <asp:GridView ID="gvHeatmap" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvHeatmap_RowDataBound" EmptyDataText="No data found.">
        <Columns>
            <asp:BoundField DataField="ReviewPeriod" HeaderText="Review Period" />
            <asp:BoundField DataField="AverageScore" HeaderText="Avg. Score" DataFormatString="{0:N2}" />
            <asp:TemplateField HeaderText="Performance Level">
                <ItemTemplate>
                    <asp:Label ID="lblPerformance" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />

        </div>

        <div>

            <h2>Performance Progress Bars</h2>

    <label>Enter Employee ID:</label>
    <asp:TextBox ID="TextBox1" runat="server" Width="150px" />
    <asp:Button ID="btnShow" runat="server" Text="Show Progress" OnClick="btnShow_Click" /><br /><br />

    <asp:Repeater ID="rptPerformance" runat="server">
        <ItemTemplate>
            <div>
                <strong>Review Period:</strong> <%# Eval("ReviewPeriod") %><br />
                <strong>Score:</strong> <%# Eval("AverageScore")%> / 5<br />
                <div class="progress-container">
                    <div class="progress-bar" style='<%# GetBarStyle(Eval("AverageScore")) %>'>
                        <%# Eval("AverageScore") %>
                    </div>
                </div>
                <br />
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="Label1" runat="server" ForeColor="Red" />

        </div>
    </form>
</body>
</html>
