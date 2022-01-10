<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPelajar_peperiksaan.aspx.vb" Inherits="KPP_MS.admin_laporanPelajar_peperikssan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            height: 220px;
            font-size: 9px;
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


            var chart = AmCharts.makeChart("chartdivLPUO", {
                "theme": "light",
                "type": "serial",
                "startDuration": 2,
                "dataProvider": [{
                    "country": "A+",
                    "visits": count40,
                    "color": "#0040ff"
                }, {
                    "country": "A",
                    "visits": count39,
                    "color": "#00bfff"
                }, {
                    "country": "A-",
                    "visits": count38,
                    "color": "#00ffff"
                }, {
                    "country": "B+",
                    "visits": count37,
                    "color": "#00ffbf"
                }, {
                    "country": "B",
                    "visits": count36,
                    "color": "#00ff80"
                }, {
                    "country": "B-",
                    "visits": count35,
                    "color": "#00ff40"
                }, {
                    "country": "C+",
                    "visits": count34,
                    "color": "#00ff00"
                }, {
                    "country": "C",
                    "visits": count33,
                    "color": "#40ff00"
                }, {
                    "country": "D",
                    "visits": count32,
                    "color": "#80ff00"
                }, {
                    "country": "E",
                    "visits": count31,
                    "color": "#bfff00"
                }, {
                    "country": "G",
                    "visits": count30,
                    "color": "#ffff00"
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

    <style>
        .sc3::-webkit-scrollbar {
            height: 8px;
        }

        .sc3::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc3::-webkit-scrollbar-thumb {
            background-color: #929B9E;
            border-radius: 3px;
        }

        .sc4::-webkit-scrollbar {
            width: 10px;
        }

        .sc4::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc4::-webkit-scrollbar-thumb {
            background-color: #929B9E;
        }
    </style>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
        <%--Breadcrum--%>
        <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
            Menu &nbsp; : &nbsp; Report &nbsp; / &nbsp; Examination Report
        </div>
    </div>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
        <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">

            <div class="w3-text-black" style="text-align: left; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass=" btn btn-default font " Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Insitutions : </asp:Label>
                <asp:DropDownList ID="ddlCampus" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Campus : </asp:Label>
                <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Examination : </asp:Label>
                <asp:DropDownList ID="ddlExam" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
                <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Course : </asp:Label>
                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
                <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
                <button id="btnExport" runat="server" class="btn btn-info" style="top: 1vw; margin-right: 1vw; display: inline-block; font-size: 0.8vw">Export Data</button>
            </div>
        </div>

        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; padding-right: 1vw; height: 70vh">

            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; display: inline-block; width: 100%">
                <div id="chartdivLPUO"></div>
            </div>

            <div style="overflow-y: scroll;  height: 35vh" class="table-responsive sc4 font">
                <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
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

                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="StudentName" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="SubjectName" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="Class" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Marks" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="marks" class="id1" runat="server" Text='<%# Eval("marks") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Grade" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="grade" class="id1" runat="server" Text='<%# Eval("grade") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Examination Result Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>
    </div>


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
</asp:Content>
