﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengarah_laporan_peperiksaan_table.ascx.vb" Inherits="KPP_MS.pengarah_laporan_peperiksaan_table" %>

<script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
<script src="https://www.amcharts.com/lib/3/serial.js"></script>
<script src="https://www.amcharts.com/lib/3/plugins/export/export.min.js"></script>
<link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
<script src="js/sidegraph_light.js"></script>

<script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
<script src="https://www.amcharts.com/lib/3/serial.js"></script>
<script src="https://www.amcharts.com/lib/3/plugins/export/export.min.js"></script>
<link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
<script src="https://www.amcharts.com/lib/3/themes/light.js"></script>

<style>
    #chartdivLPUO {
        width: 100%;
        height: 300px;
        font-size: 11px;
    }
</style>

<script>
    $(document).ready(function () {

        var count40 = document.getElementById('<%= count400.ClientID %>').value;
        var count39 = document.getElementById('<%= countmore390.ClientID %>').value;
        var count38 = document.getElementById('<%= countmore380.ClientID %>').value;
        var count37 = document.getElementById('<%= countmore370.ClientID %>').value;
        var count36 = document.getElementById('<%= countmore360.ClientID %>').value;
        var count35 = document.getElementById('<%= countmore350.ClientID %>').value;
        var count34 = document.getElementById('<%= countmore340.ClientID %>').value;
        var count33 = document.getElementById('<%= countmore330.ClientID %>').value;
        var count32 = document.getElementById('<%= countmore320.ClientID %>').value;
        var count31 = document.getElementById('<%= countmore310.ClientID %>').value;
        var count30 = document.getElementById('<%= countmore300.ClientID %>').value;
        var count29 = document.getElementById('<%= countmore290.ClientID %>').value;
        var count28 = document.getElementById('<%= countmore280.ClientID %>').value;
        var count27 = document.getElementById('<%= countmore270.ClientID %>').value;
        var count26 = document.getElementById('<%= countmore260.ClientID %>').value;
        var count25 = document.getElementById('<%= countmore250.ClientID %>').value;
        var countless25 = document.getElementById('<%= countless250.ClientID %>').value;


        var chart = AmCharts.makeChart("chartdivLPUO", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "<2.50",
                "visits": countless25,
                "color": "#ff4000"
            }, {
                "country": " >=2.50",
                "visits": count25,
                "color": "#ff8000"
            }, {
                "country": " >=2.60",
                "visits": count26,
                "color": "#ffbf00"
            }, {
                "country": " >=2.70",
                "visits": count27,
                "color": "#ffff00"
            }, {
                "country": " >=2.80",
                "visits": count28,
                "color": "#bfff00"
            }, {
                "country": " >=2.90",
                "visits": count29,
                "color": "#80ff00"
            }, {
                "country": " >=3.00",
                "visits": count30,
                "color": "#40ff00"
            }, {
                "country": " >=3.10",
                "visits": count31,
                "color": "#00ff00"
            }, {
                "country": " >=3.20",
                "visits": count32,
                "color": "#00ff40"
            }, {
                "country": " >=3.30",
                "visits": count33,
                "color": "#00ff80"
            }, {
                "country": " >=3.40",
                "visits": count34,
                "color": "#00ffbf"
            }, {
                "country": " >=3.50",
                "visits": count35,
                "color": "#00ffff"
            }, {
                "country": " >=3.60",
                "visits": count36,
                "color": "#00bfff"
            }, {
                "country": " >=3.70",
                "visits": count37,
                "color": "#0040ff"
            }, {
                "country": " >=3.80",
                "visits": count38,
                "color": "#8000ff"
            }, {
                "country": " >=3.90",
                "visits": count39,
                "color": "#bf00ff"
            }, {
                "country": "4.00",
                "visits": count40,
                "color": "#ff00ff"
            }],
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillColorsField": "color",
                "fillAlphas": 1,
                "lineAlpha": 0.1,
                "labelText": "[[value]]",
                "type": "column",
                "valueField": "visits"
            }],
            "depth3D": 20,
            "angle": 30,
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "country",
            "categoryAxis": {
                "gridPosition": "start",
                "labelRotation": 0
            },

        });
    });
</script>

<br />

<style>
    #chartdivLPUO {
        width: 100%;
        height: 300px;
        font-size: 11px;
    }

    .ddl {
        border-radius: 25px;
    }

    .centerHeader {
        text-align: center;
    }

    .lblAbsent {
        font-size: 15px;
    }

    .lblAttend {
        font-size: 15px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Student Ranking List</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlExam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <p></p>
        </div>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 15px">
        <div class="col-md-12">
            <p style="background-color: #800000; display: inline-block; width: 90%; border-radius: 25px">Examination Results :
                <asp:Label ID="lblExamination" runat="server" Text=""></asp:Label></p>
            <div id="chartdivLPUO"></div>
            <br />
        </div>
    </div>
    <br />
    <div style="overflow-y: scroll; overflow-x: hidden; height: 420px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>

                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="1000">
                    <ItemTemplate>
                        <asp:Label ID="StudentName" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student MyKad" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="StudentMyKad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Class" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="Class" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gpa x Credit " ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="countGPA" class="id1" runat="server" Text='<%# Eval("gpa X credithour") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Academic PNG" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="GPA" class="id1" runat="server" Text='<%# Eval("Total GPA", "{0:n}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <br />
    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px; text-align: left">
        <button id="BtnExport" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Export">Export <i class="fa fa-list-alt w3-large w3-text-white"></i></button>
    </div>
</div>
<br />


<asp:HiddenField ClientIDMode="static" ID="count400" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore390" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore380" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore370" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore360" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore350" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore340" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore330" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore320" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore310" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore300" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore290" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore280" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore270" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore260" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countmore250" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="countless250" runat="server" />


