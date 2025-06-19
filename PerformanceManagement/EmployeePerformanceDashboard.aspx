<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePerformanceDashboard.aspx.cs" Inherits="LeavePolicy.PerformanceManagement.EmployeePerformanceDashboard" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Performance Dashboard</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        .dial {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            border: 10px solid lightgray;
            position: relative;
            margin: 10px;
        }
        .dial .value {
            position: absolute;
            width: 100%;
            top: 35%;
            text-align: center;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Performance Dashboard</h2>

    <label>Enter Employee ID:</label>
    <asp:TextBox ID="txtEmpID" runat="server" CssClass="form-control" Width="200px" />
    <asp:Button ID="btnLoad" runat="server" Text="Load Dashboard" CssClass="btn btn-primary mt-2" OnClick="btnLoad_Click" /><br />

    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" /><br />

    <%-- Chart.js Section --%>
    <canvas id="scoreChart" width="800" height="300"></canvas>
    <asp:Literal ID="litChartScript" runat="server" />

    <%-- Heatmap GridView --%>
    <h4>Performance Heatmap</h4>
    <asp:GridView ID="gvHeatmap" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvHeatmap_RowDataBound" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="ReviewPeriod" HeaderText="Review Period" />
            <asp:BoundField DataField="AverageScore" HeaderText="Score" />
            <asp:TemplateField HeaderText="Level">
                <ItemTemplate>
                    <asp:Label ID="lblPerformance" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <%-- Progress Bars Section --%>
    <h4>Performance as Progress Bars</h4>
    <asp:Repeater ID="rptProgress" runat="server">
        <ItemTemplate>
            <div>
                <b><%# Eval("ReviewPeriod") %>:</b>
                <div class="progress mb-2" style="width: 400px;">
                    <div class="progress-bar" style='<%# GetBarStyle(Eval("AverageScore")) %>' role="progressbar">
                        <%# Eval("AverageScore") %>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <%-- Circular Dials Section --%>
    <h4>Performance Dials</h4>
    <asp:Repeater ID="rptDials" runat="server">
        <ItemTemplate>
            <div class="dial" style='<%# GetDialStyle(Eval("AverageScore")) %>'>
                <div class="value"><%# Eval("AverageScore") %></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

        </div>
    </form>
</body>
</html>
