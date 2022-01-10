<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengarah_laporan_peperiksaan_kursus_table.ascx.vb" Inherits="KPP_MS.pengarah_laporan_peperiksaan_kursus_table" %>

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
    #table1 {
        height: 250px;
        font-size: 10px;
    }

    #table2 {
        height: 250px;
        font-size: 10px;
    }

    #table3 {
        height: 250px;
        font-size: 10px;
    }

    #table4 {
        height: 250px;
        font-size: 10px;
    }

    #table5 {
        height: 250px;
        font-size: 10px;
    }

    #table6 {
        height: 250px;
        font-size: 10px;
    }
</style>

<script>
    $(document).ready(function () {

        var displaygraph1 = document.getElementById('<%= graph1.ClientID %>').value;
        var displaygraph2 = document.getElementById('<%= graph2.ClientID %>').value;
        var displaygraph3 = document.getElementById('<%= graph3.ClientID %>').value;
        var displaygraph4 = document.getElementById('<%= graph4.ClientID %>').value;
        var displaygraph5 = document.getElementById('<%= graph5.ClientID %>').value;
         var displaygraph6 = document.getElementById('<%= graph6.ClientID %>').value;

        if (displaygraph1 == 0) {
            document.getElementById("graph1display").style.display = "none";
        }
        else {
            document.getElementById("graph1display").style.display = "block";
        }

        if (displaygraph2 == 0) {
            document.getElementById("graph2display").style.display = "none";
        }
        else {
            document.getElementById("graph2display").style.display = "block";
        }

        if (displaygraph3 == 0) {
            document.getElementById("graph3display").style.display = "none";
        }
        else {
            document.getElementById("graph3display").style.display = "block";
        }

        if (displaygraph4 == 0) {
            document.getElementById("graph4display").style.display = "none";
        }
        else {
            document.getElementById("graph4display").style.display = "block";
        }

        if (displaygraph5 == 0) {
            document.getElementById("graph5display").style.display = "none";
        }
        else {
            document.getElementById("graph5display").style.display = "block";
        }

        if (displaygraph6 == 0) {
            document.getElementById("graph6display").style.display = "none";
        }
        else {
            document.getElementById("graph6display").style.display = "block";
        }


    });

</script>

<script>
    $(document).ready(function () {

        var aplus = document.getElementById('<%= table1_countaplus.ClientID %>').value;
        var aa = document.getElementById('<%= table1_countaa.ClientID %>').value;
        var aminus = document.getElementById('<%= table1_countaminus.ClientID %>').value;
        var bplus = document.getElementById('<%= table1_countbplus.ClientID %>').value;
        var bb = document.getElementById('<%= table1_countbb.ClientID %>').value;
        var bminus = document.getElementById('<%= table1_countbminus.ClientID %>').value;
        var cplus = document.getElementById('<%= table1_countcplus.ClientID %>').value;
        var cc = document.getElementById('<%= table1_countcc.ClientID %>').value;
        var dd = document.getElementById('<%= table1_countdd.ClientID %>').value;
        var ee = document.getElementById('<%= table1_countee.ClientID %>').value;
        var gg = document.getElementById('<%= table1_countgg.ClientID %>').value;

        var chart = AmCharts.makeChart("table1", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "A+",
                "visits": aplus,
                "color": "#0040ff"
            }, {
                "country": "A",
                "visits": aa,
                "color": "#00bfff"
            }, {
                "country": "A-",
                "visits": aminus,
                "color": "#00ffff"
            }, {
                "country": "B+",
                "visits": bplus,
                "color": "#00ffbf"
            }, {
                "country": "B",
                "visits": bb,
                "color": "#00ff80"
            }, {
                "country": "B-",
                "visits": bminus,
                "color": "#00ff40"
            }, {
                "country": "C+",
                "visits": cplus,
                "color": "#00ff00"
            }, {
                "country": "C",
                "visits": cc,
                "color": "#40ff00"
            }, {
                "country": "D",
                "visits": dd,
                "color": "#80ff00"
            }, {
                "country": "E",
                "visits": ee,
                "color": "#bfff00"
            }, {
                "country": "G",
                "visits": gg,
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
            }
        });

    });
</script>

<script>
    $(document).ready(function () {

        var aplus = document.getElementById('<%= table2_countaplus.ClientID %>').value;
        var aa = document.getElementById('<%= table2_countaa.ClientID %>').value;
        var aminus = document.getElementById('<%= table2_countaminus.ClientID %>').value;
        var bplus = document.getElementById('<%= table2_countbplus.ClientID %>').value;
        var bb = document.getElementById('<%= table2_countbb.ClientID %>').value;
        var bminus = document.getElementById('<%= table2_countbminus.ClientID %>').value;
        var cplus = document.getElementById('<%= table2_countcplus.ClientID %>').value;
        var cc = document.getElementById('<%= table2_countcc.ClientID %>').value;
        var dd = document.getElementById('<%= table2_countdd.ClientID %>').value;
        var ee = document.getElementById('<%= table2_countee.ClientID %>').value;
        var gg = document.getElementById('<%= table2_countgg.ClientID %>').value;


        var chart = AmCharts.makeChart("table2", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "A+",
                "visits": aplus,
                "color": "#0040ff"
            }, {
                "country": "A",
                "visits": aa,
                "color": "#00bfff"
            }, {
                "country": "A-",
                "visits": aminus,
                "color": "#00ffff"
            }, {
                "country": "B+",
                "visits": bplus,
                "color": "#00ffbf"
            }, {
                "country": "B",
                "visits": bb,
                "color": "#00ff80"
            }, {
                "country": "B-",
                "visits": bminus,
                "color": "#00ff40"
            }, {
                "country": "C+",
                "visits": cplus,
                "color": "#00ff00"
            }, {
                "country": "C",
                "visits": cc,
                "color": "#40ff00"
            }, {
                "country": "D",
                "visits": dd,
                "color": "#80ff00"
            }, {
                "country": "E",
                "visits": ee,
                "color": "#bfff00"
            }, {
                "country": "G",
                "visits": gg,
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
            }

        });
    });
</script>

<script>
    $(document).ready(function () {

        var aplus = document.getElementById('<%= table3_countaplus.ClientID %>').value;
        var aa = document.getElementById('<%= table3_countaa.ClientID %>').value;
        var aminus = document.getElementById('<%= table3_countaminus.ClientID %>').value;
        var bplus = document.getElementById('<%= table3_countbplus.ClientID %>').value;
        var bb = document.getElementById('<%= table3_countbb.ClientID %>').value;
        var bminus = document.getElementById('<%= table3_countbminus.ClientID %>').value;
        var cplus = document.getElementById('<%= table3_countcplus.ClientID %>').value;
        var cc = document.getElementById('<%= table3_countcc.ClientID %>').value;
        var dd = document.getElementById('<%= table3_countdd.ClientID %>').value;
        var ee = document.getElementById('<%= table3_countee.ClientID %>').value;
        var gg = document.getElementById('<%= table3_countgg.ClientID %>').value;


        var chart = AmCharts.makeChart("table3", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "A+",
                "visits": aplus,
                "color": "#0040ff"
            }, {
                "country": "A",
                "visits": aa,
                "color": "#00bfff"
            }, {
                "country": "A-",
                "visits": aminus,
                "color": "#00ffff"
            }, {
                "country": "B+",
                "visits": bplus,
                "color": "#00ffbf"
            }, {
                "country": "B",
                "visits": bb,
                "color": "#00ff80"
            }, {
                "country": "B-",
                "visits": bminus,
                "color": "#00ff40"
            }, {
                "country": "C+",
                "visits": cplus,
                "color": "#00ff00"
            }, {
                "country": "C",
                "visits": cc,
                "color": "#40ff00"
            }, {
                "country": "D",
                "visits": dd,
                "color": "#80ff00"
            }, {
                "country": "E",
                "visits": ee,
                "color": "#bfff00"
            }, {
                "country": "G",
                "visits": gg,
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
            }

        });
    });
</script>

<script>
    $(document).ready(function () {

        var aplus = document.getElementById('<%= table4_countaplus.ClientID %>').value;
        var aa = document.getElementById('<%= table4_countaa.ClientID %>').value;
        var aminus = document.getElementById('<%= table4_countaminus.ClientID %>').value;
        var bplus = document.getElementById('<%= table4_countbplus.ClientID %>').value;
        var bb = document.getElementById('<%= table4_countbb.ClientID %>').value;
        var bminus = document.getElementById('<%= table4_countbminus.ClientID %>').value;
        var cplus = document.getElementById('<%= table4_countcplus.ClientID %>').value;
        var cc = document.getElementById('<%= table4_countcc.ClientID %>').value;
        var dd = document.getElementById('<%= table4_countdd.ClientID %>').value;
        var ee = document.getElementById('<%= table4_countee.ClientID %>').value;
        var gg = document.getElementById('<%= table4_countgg.ClientID %>').value;


        var chart = AmCharts.makeChart("table4", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "A+",
                "visits": aplus,
                "color": "#0040ff"
            }, {
                "country": "A",
                "visits": aa,
                "color": "#00bfff"
            }, {
                "country": "A-",
                "visits": aminus,
                "color": "#00ffff"
            }, {
                "country": "B+",
                "visits": bplus,
                "color": "#00ffbf"
            }, {
                "country": "B",
                "visits": bb,
                "color": "#00ff80"
            }, {
                "country": "B-",
                "visits": bminus,
                "color": "#00ff40"
            }, {
                "country": "C+",
                "visits": cplus,
                "color": "#00ff00"
            }, {
                "country": "C",
                "visits": cc,
                "color": "#40ff00"
            }, {
                "country": "D",
                "visits": dd,
                "color": "#80ff00"
            }, {
                "country": "E",
                "visits": ee,
                "color": "#bfff00"
            }, {
                "country": "G",
                "visits": gg,
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
            }
        });
    });
</script>

<script>
    $(document).ready(function () {

        var aplus = document.getElementById('<%= table5_countaplus.ClientID %>').value;
        var aa = document.getElementById('<%= table5_countaa.ClientID %>').value;
        var aminus = document.getElementById('<%= table5_countaminus.ClientID %>').value;
        var bplus = document.getElementById('<%= table5_countbplus.ClientID %>').value;
        var bb = document.getElementById('<%= table5_countbb.ClientID %>').value;
        var bminus = document.getElementById('<%= table5_countbminus.ClientID %>').value;
        var cplus = document.getElementById('<%= table5_countcplus.ClientID %>').value;
        var cc = document.getElementById('<%= table5_countcc.ClientID %>').value;
        var dd = document.getElementById('<%= table5_countdd.ClientID %>').value;
        var ee = document.getElementById('<%= table5_countee.ClientID %>').value;
        var gg = document.getElementById('<%= table5_countgg.ClientID %>').value;


        var chart = AmCharts.makeChart("table5", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "A+",
                "visits": aplus,
                "color": "#0040ff"
            }, {
                "country": "A",
                "visits": aa,
                "color": "#00bfff"
            }, {
                "country": "A-",
                "visits": aminus,
                "color": "#00ffff"
            }, {
                "country": "B+",
                "visits": bplus,
                "color": "#00ffbf"
            }, {
                "country": "B",
                "visits": bb,
                "color": "#00ff80"
            }, {
                "country": "B-",
                "visits": bminus,
                "color": "#00ff40"
            }, {
                "country": "C+",
                "visits": cplus,
                "color": "#00ff00"
            }, {
                "country": "C",
                "visits": cc,
                "color": "#40ff00"
            }, {
                "country": "D",
                "visits": dd,
                "color": "#80ff00"
            }, {
                "country": "E",
                "visits": ee,
                "color": "#bfff00"
            }, {
                "country": "G",
                "visits": gg,
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
            }

        });
    });
</script>

<script>
    $(document).ready(function () {

        var aplus = document.getElementById('<%= table6_countaplus.ClientID %>').value;
        var aa = document.getElementById('<%= table6_countaa.ClientID %>').value;
        var aminus = document.getElementById('<%= table6_countaminus.ClientID %>').value;
        var bplus = document.getElementById('<%= table6_countbplus.ClientID %>').value;
        var bb = document.getElementById('<%= table6_countbb.ClientID %>').value;
        var bminus = document.getElementById('<%= table6_countbminus.ClientID %>').value;
        var cplus = document.getElementById('<%= table6_countcplus.ClientID %>').value;
        var cc = document.getElementById('<%= table6_countcc.ClientID %>').value;
        var dd = document.getElementById('<%= table6_countdd.ClientID %>').value;
        var ee = document.getElementById('<%= table6_countee.ClientID %>').value;
        var gg = document.getElementById('<%= table6_countgg.ClientID %>').value;


        var chart = AmCharts.makeChart("table6", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "A+",
                "visits": aplus,
                "color": "#0040ff"
            }, {
                "country": "A",
                "visits": aa,
                "color": "#00bfff"
            }, {
                "country": "A-",
                "visits": aminus,
                "color": "#00ffff"
            }, {
                "country": "B+",
                "visits": bplus,
                "color": "#00ffbf"
            }, {
                "country": "B",
                "visits": bb,
                "color": "#00ff80"
            }, {
                "country": "B-",
                "visits": bminus,
                "color": "#00ff40"
            }, {
                "country": "C+",
                "visits": cplus,
                "color": "#00ff00"
            }, {
                "country": "C",
                "visits": cc,
                "color": "#40ff00"
            }, {
                "country": "D",
                "visits": dd,
                "color": "#80ff00"
            }, {
                "country": "E",
                "visits": ee,
                "color": "#bfff00"
            }, {
                "country": "G",
                "visits": gg,
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
            }

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
        height: 8px;
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
        Menu &nbsp; : &nbsp; Examination Report &nbsp; / &nbsp; Report By Course
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">

        <div class="w3-text-black" style="text-align: left; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass=" btn btn-default font " style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Insitutions : </asp:Label>
                <asp:DropDownList ID="ddlCampus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Campus : </asp:Label>
                <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Examination : </asp:Label>
            <asp:DropDownList ID="ddlExam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" CssClass=" btn btn-default font" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Course : </asp:Label>
            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" CssClass=" btn btn-default font" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Subject : </asp:Label>
            <asp:DropDownList ID="ddlBahasa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBahasa_SelectedIndexChanged" Enabled="false" CssClass=" btn btn-default font" style="font-size:0.8vw"></asp:DropDownList>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; padding-right:1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" class="sc4">
        <div hidden="hidden" class="table-responsive font" style="overflow-y: scroll; height: 62vh">

            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="subject_ID" BorderStyle="None" GridLines="None"
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

                    <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subject StudentYear" ItemStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="subject_StudentYear" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jumlah Pelajar" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="StudentName" class="id1" runat="server" Text='<%# Eval("Jumlah Pelajar") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A+" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="aplus" class="id1" runat="server" Text='<%# Eval("A+") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentageaplus" class="id1" runat="server" Text='<%# Eval("%A+") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="aa" class="id1" runat="server" Text='<%# Eval("A") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentageaa" class="id1" runat="server" Text='<%# Eval("%A") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="A-" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="aminus" class="id1" runat="server" Text='<%# Eval("A-") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentageaminus" class="id1" runat="server" Text='<%# Eval("%A-") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="B+" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="bplus" class="id1" runat="server" Text='<%# Eval("B+") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagebplus" class="id1" runat="server" Text='<%# Eval("%B+") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="B" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="bb" class="id1" runat="server" Text='<%# Eval("B") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagebb" class="id1" runat="server" Text='<%# Eval("%B") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="B-" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="bminus" class="id1" runat="server" Text='<%# Eval("B-") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagebminus" class="id1" runat="server" Text='<%# Eval("%B-") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="C+" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="cplus" class="id1" runat="server" Text='<%# Eval("C+") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagecplus" class="id1" runat="server" Text='<%# Eval("%C+") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="C" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="cc" class="id1" runat="server" Text='<%# Eval("C") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagecc" class="id1" runat="server" Text='<%# Eval("%C") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="D" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="dd" class="id1" runat="server" Text='<%# Eval("D") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagedd" class="id1" runat="server" Text='<%# Eval("%D") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="E" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="ee" class="id1" runat="server" Text='<%# Eval("E") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentageee" class="id1" runat="server" Text='<%# Eval("%E") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="G" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="gg" class="id1" runat="server" Text='<%# Eval("G") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="%" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="percentagegg" class="id1" runat="server" Text='<%# Eval("%G") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <div id="graph1display" class="w3-text-white font" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; width: 100%">
            <p style="background-color: #567572FF; padding-left: 1vw;" class="w3-card-2">
                <asp:Label ID="lblKursus1" runat="server" Text=""></asp:Label>
            </p>
            <div id="table1"></div>
        </div>

        <div id="graph2display" class="w3-text-white font" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; width: 100%">
            <p style="background-color: #567572FF; padding-left: 1vw;" class="w3-card-2">
                <asp:Label ID="lblKursus2" runat="server" Text=""></asp:Label>
            </p>
            <div id="table2"></div>
        </div>

        <div id="graph3display" class="w3-text-white font" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; width: 100%">
            <p style="background-color: #567572FF; padding-left: 1vw;" class="w3-card-2">
                <asp:Label ID="lblKursus3" runat="server" Text=""></asp:Label>
            </p>
            <div id="table3"></div>
        </div>

        <div id="graph4display" class="w3-text-white font" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; width: 100%">
            <p style="background-color: #567572FF; padding-left: 1vw;" class="w3-card-2">
                <asp:Label ID="lblKursus4" runat="server" Text=""></asp:Label>
            </p>
            <div id="table4"></div>
        </div>

        <div id="graph5display" class="w3-text-white font" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; width: 100%">
            <p style="background-color: #567572FF; padding-left: 1vw;" class="w3-card-2">
                <asp:Label ID="lblKursus5" runat="server" Text=""></asp:Label>
            </p>
            <div id="table5"></div>
        </div>

        <div id="graph6display" class="w3-text-white font" style="text-align: left; padding-left: 1vw; padding-bottom: 1vh; width: 100%">
            <p style="background-color: #567572FF; padding-left: 1vw;" class="w3-card-2">
                <asp:Label ID="lblKursus6" runat="server" Text=""></asp:Label>
            </p>
            <div id="table6"></div>
        </div>

    </div>
</div>

<asp:HiddenField ClientIDMode="static" ID="graph1" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="graph2" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="graph3" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="graph4" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="graph5" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="graph6" runat="server" />

<asp:HiddenField ClientIDMode="static" ID="table1_countaplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countaa" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countaminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countbplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countbb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countbminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countcplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countcc" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countdd" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countee" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table1_countgg" runat="server" />

<asp:HiddenField ClientIDMode="static" ID="table2_countaplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countaa" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countaminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countbplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countbb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countbminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countcplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countcc" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countdd" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countee" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table2_countgg" runat="server" />

<asp:HiddenField ClientIDMode="static" ID="table3_countaplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countaa" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countaminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countbplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countbb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countbminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countcplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countcc" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countdd" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countee" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table3_countgg" runat="server" />

<asp:HiddenField ClientIDMode="static" ID="table4_countaplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countaa" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countaminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countbplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countbb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countbminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countcplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countcc" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countdd" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countee" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table4_countgg" runat="server" />

<asp:HiddenField ClientIDMode="static" ID="table5_countaplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countaa" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countaminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countbplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countbb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countbminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countcplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countcc" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countdd" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countee" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table5_countgg" runat="server" />

<asp:HiddenField ClientIDMode="static" ID="table6_countaplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countaa" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countaminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countbplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countbb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countbminus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countcplus" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countcc" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countdd" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countee" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="table6_countgg" runat="server" />