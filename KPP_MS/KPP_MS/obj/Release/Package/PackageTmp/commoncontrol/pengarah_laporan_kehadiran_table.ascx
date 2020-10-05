<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengarah_laporan_kehadiran_table.ascx.vb" Inherits="KPP_MS.pengarah_laporan_kehadiran_table" %>

<script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
<script src="https://www.amcharts.com/lib/3/serial.js"></script>
<script src="https://www.amcharts.com/lib/3/plugins/export/export.min.js"></script>
<link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
<script src="https://www.amcharts.com/lib/3/themes/light.js"></script>

<style>
    #chartdivLPUT {
        width: 100%;
        height: 300px;
        font-size: 11px;
    }

    #chartdivLPUO {
        width: 100%;
        height: 300px;
        font-size: 11px;
    }
</style>

<script>


    $(document).ready(function () {

        var JAN = document.getElementById('<%= attendanceJan.ClientID %>').value;
        var FEB = document.getElementById('<%= attendanceFeb.ClientID %>').value;
        var MAC = document.getElementById('<%= attendanceMac.ClientID %>').value;
        var APR = document.getElementById('<%= attendanceApr.ClientID %>').value;
        var MAY = document.getElementById('<%= attendanceMay.ClientID %>').value;
        var JUN = document.getElementById('<%= attendanceJun.ClientID %>').value;
        var JUL = document.getElementById('<%= attendanceJul.ClientID %>').value;
        var AUG = document.getElementById('<%= attendanceAug.ClientID %>').value;
        var SEP = document.getElementById('<%= attendanceSep.ClientID %>').value;
        var OCT = document.getElementById('<%= attendanceOct.ClientID %>').value;
        var NOV = document.getElementById('<%= attendanceNov.ClientID %>').value;
        var DEC = document.getElementById('<%= attendanceDec.ClientID %>').value;

        var chart = AmCharts.makeChart("chartdivLPUO", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": [{
                "country": "JANUARY",
                "visits": JAN,
                "color": "#2A0CD0"
            }, {
                "country": "FEBRUARY",
                "visits": FEB,
                "color": "#8A2BE2"
            }, {
                "country": "MARCH",
                "visits": MAC,
                "color": "#0D52D1"
            }, {
                "country": "APRIL",
                "visits": APR,
                "color": "#0D8ECF"
            }, {
                "country": "MAY",
                "visits": MAY,
                "color": "#04D215"
            }, {
                "country": "JUNE",
                "visits": JUN,
                "color": "#B0DE09"
            }, {
                "country": "JULY",
                "visits": JUL,
                "color": "#F8FF01"
            }, {
                "country": "AUGUST",
                "visits": AUG,
                "color": "#FCD202"
            }, {
                "country": "SEPTEMBER",
                "visits": SEP,
                "color": "#FF9E01"
            }, {
                "country": "OCTOBER",
                "visits": OCT,
                "color": "#FF6600"
            }, {
                "country": "NOVEMBER",
                "visits": NOV,
                "color": "#FF0F00"
            }, {
                "country": "DECEMBER",
                "visits": DEC,
                "color": "#FF0F00"
            }],
            "valueAxes": [{
                "stackType": "regular",
                "axisAlpha": 0,
                "gridAlpha": 0,
                "labelsEnabled": false,
                "position": "left"
            }],
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]%</b>",
                "fillColorsField": "color",
                "fontSize": 13,
                "labelText": "[[value]]%",
                "fillAlphas": 1,
                "lineAlpha": 0.1,
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

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

<script>
    $(document).ready(function () {

        var totalClass = 0;
        totalClass = document.getElementById('<%= ddlSelect_Class.ClientID %>').length;

        if (document.getElementById('<%= ddlSubjectID.ID %>').length > 1) {
            totalClass = document.getElementById('<%= ddlSubjectID.ID %>').length;
        }

        var namasubjek1 = document.getElementById('<%= subjectName1.ClientID %>').value;
        var namasubjek2 = document.getElementById('<%= subjectName2.ClientID %>').value;
        var namasubjek3 = document.getElementById('<%= subjectName3.ClientID %>').value;
        var namasubjek4 = document.getElementById('<%= subjectName4.ClientID %>').value;
        var namasubjek5 = document.getElementById('<%= subjectName5.ClientID %>').value;
        var namasubjek6 = document.getElementById('<%= subjectName6.ClientID %>').value;
        var namasubjek7 = document.getElementById('<%= subjectName7.ClientID %>').value;
        var namasubjek8 = document.getElementById('<%= subjectName8.ClientID %>').value;
        var namasubjek9 = document.getElementById('<%= subjectName9.ClientID %>').value;
        var namasubjek10 = document.getElementById('<%= subjectName10.ClientID %>').value;
        var namasubjek11 = document.getElementById('<%= subjectName11.ClientID %>').value;
        var namasubjek12 = document.getElementById('<%= subjectName12.ClientID %>').value;
        var namasubjek13 = document.getElementById('<%= subjectName13.ClientID %>').value;
        var namasubjek14 = document.getElementById('<%= subjectName14.ClientID %>').value;
        var namasubjek15 = document.getElementById('<%= subjectName15.ClientID %>').value;
        var namasubjek16 = document.getElementById('<%= subjectName16.ClientID %>').value;
        var namasubjek17 = document.getElementById('<%= subjectName17.ClientID %>').value;
        var namasubjek18 = document.getElementById('<%= subjectName18.ClientID %>').value;
        var namasubjek19 = document.getElementById('<%= subjectName19.ClientID %>').value;
        var namasubjek20 = document.getElementById('<%= subjectName20.ClientID %>').value;
        var namasubjek21 = document.getElementById('<%= subjectName21.ClientID %>').value;
        var namasubjek22 = document.getElementById('<%= subjectName22.ClientID %>').value;
        var namasubjek23 = document.getElementById('<%= subjectName23.ClientID %>').value;
        var namasubjek24 = document.getElementById('<%= subjectName24.ClientID %>').value;
        var namasubjek25 = document.getElementById('<%= subjectName25.ClientID %>').value;
        var namasubjek26 = document.getElementById('<%= subjectName26.ClientID %>').value;
        var namasubjek27 = document.getElementById('<%= subjectName27.ClientID %>').value;
        var namasubjek28 = document.getElementById('<%= subjectName28.ClientID %>').value;
        var namasubjek29 = document.getElementById('<%= subjectName29.ClientID %>').value;
        var namasubjek30 = document.getElementById('<%= subjectName30.ClientID %>').value;
        var namasubjek31 = document.getElementById('<%= subjectName31.ClientID %>').value;
        var namasubjek32 = document.getElementById('<%= subjectName32.ClientID %>').value;
        var namasubjek33 = document.getElementById('<%= subjectName33.ClientID %>').value;
        var namasubjek34 = document.getElementById('<%= subjectName34.ClientID %>').value;
        var namasubjek35 = document.getElementById('<%= subjectName35.ClientID %>').value;
        var namasubjek36 = document.getElementById('<%= subjectName36.ClientID %>').value;
        var namasubjek37 = document.getElementById('<%= subjectName37.ClientID %>').value;
        var namasubjek38 = document.getElementById('<%= subjectName38.ClientID %>').value;
        var namasubjek39 = document.getElementById('<%= subjectName39.ClientID %>').value;
        var namasubjek40 = document.getElementById('<%= subjectName40.ClientID %>').value;
        var namasubjek41 = document.getElementById('<%= subjectName41.ClientID %>').value;
        var namasubjek42 = document.getElementById('<%= subjectName42.ClientID %>').value;
        var namasubjek43 = document.getElementById('<%= subjectName43.ClientID %>').value;
        var namasubjek44 = document.getElementById('<%= subjectName44.ClientID %>').value;
        var namasubjek45 = document.getElementById('<%= subjectName45.ClientID %>').value;
        var namasubjek46 = document.getElementById('<%= subjectName46.ClientID %>').value;
        var namasubjek47 = document.getElementById('<%= subjectName47.ClientID %>').value;
        var namasubjek48 = document.getElementById('<%= subjectName48.ClientID %>').value;
        var namasubjek49 = document.getElementById('<%= subjectName49.ClientID %>').value;
        var namasubjek50 = document.getElementById('<%= subjectName50.ClientID %>').value;

        var kehadiran1 = document.getElementById('<%= subjectAttendance1.ClientID %>').value;
        var kehadiran2 = document.getElementById('<%= subjectAttendance2.ClientID %>').value;
        var kehadiran3 = document.getElementById('<%= subjectAttendance3.ClientID %>').value;
        var kehadiran4 = document.getElementById('<%= subjectAttendance4.ClientID %>').value;
        var kehadiran5 = document.getElementById('<%= subjectAttendance5.ClientID %>').value;
        var kehadiran6 = document.getElementById('<%= subjectAttendance6.ClientID %>').value;
        var kehadiran7 = document.getElementById('<%= subjectAttendance7.ClientID %>').value;
        var kehadiran8 = document.getElementById('<%= subjectAttendance8.ClientID %>').value;
        var kehadiran9 = document.getElementById('<%= subjectAttendance9.ClientID %>').value;
        var kehadiran10 = document.getElementById('<%= subjectAttendance10.ClientID %>').value;
        var kehadiran11 = document.getElementById('<%= subjectAttendance11.ClientID %>').value;
        var kehadiran12 = document.getElementById('<%= subjectAttendance12.ClientID %>').value;
        var kehadiran13 = document.getElementById('<%= subjectAttendance13.ClientID %>').value;
        var kehadiran14 = document.getElementById('<%= subjectAttendance14.ClientID %>').value;
        var kehadiran15 = document.getElementById('<%= subjectAttendance15.ClientID %>').value;
        var kehadiran16 = document.getElementById('<%= subjectAttendance16.ClientID %>').value;
        var kehadiran17 = document.getElementById('<%= subjectAttendance17.ClientID %>').value;
        var kehadiran18 = document.getElementById('<%= subjectAttendance18.ClientID %>').value;
        var kehadiran19 = document.getElementById('<%= subjectAttendance19.ClientID %>').value;
        var kehadiran20 = document.getElementById('<%= subjectAttendance20.ClientID %>').value;
        var kehadiran21 = document.getElementById('<%= subjectAttendance21.ClientID %>').value;
        var kehadiran22 = document.getElementById('<%= subjectAttendance22.ClientID %>').value;
        var kehadiran23 = document.getElementById('<%= subjectAttendance23.ClientID %>').value;
        var kehadiran24 = document.getElementById('<%= subjectAttendance24.ClientID %>').value;
        var kehadiran25 = document.getElementById('<%= subjectAttendance25.ClientID %>').value;
        var kehadiran26 = document.getElementById('<%= subjectAttendance26.ClientID %>').value;
        var kehadiran27 = document.getElementById('<%= subjectAttendance27.ClientID %>').value;
        var kehadiran28 = document.getElementById('<%= subjectAttendance28.ClientID %>').value;
        var kehadiran29 = document.getElementById('<%= subjectAttendance29.ClientID %>').value;
        var kehadiran30 = document.getElementById('<%= subjectAttendance30.ClientID %>').value;
        var kehadiran31 = document.getElementById('<%= subjectAttendance31.ClientID %>').value;
        var kehadiran32 = document.getElementById('<%= subjectAttendance32.ClientID %>').value;
        var kehadiran33 = document.getElementById('<%= subjectAttendance33.ClientID %>').value;
        var kehadiran34 = document.getElementById('<%= subjectAttendance34.ClientID %>').value;
        var kehadiran35 = document.getElementById('<%= subjectAttendance35.ClientID %>').value;
        var kehadiran36 = document.getElementById('<%= subjectAttendance36.ClientID %>').value;
        var kehadiran37 = document.getElementById('<%= subjectAttendance37.ClientID %>').value;
        var kehadiran38 = document.getElementById('<%= subjectAttendance38.ClientID %>').value;
        var kehadiran39 = document.getElementById('<%= subjectAttendance39.ClientID %>').value;
        var kehadiran40 = document.getElementById('<%= subjectAttendance40.ClientID %>').value;
        var kehadiran41 = document.getElementById('<%= subjectAttendance41.ClientID %>').value;
        var kehadiran42 = document.getElementById('<%= subjectAttendance42.ClientID %>').value;
        var kehadiran43 = document.getElementById('<%= subjectAttendance43.ClientID %>').value;
        var kehadiran44 = document.getElementById('<%= subjectAttendance44.ClientID %>').value;
        var kehadiran45 = document.getElementById('<%= subjectAttendance45.ClientID %>').value;
        var kehadiran46 = document.getElementById('<%= subjectAttendance46.ClientID %>').value;
        var kehadiran47 = document.getElementById('<%= subjectAttendance47.ClientID %>').value;
        var kehadiran48 = document.getElementById('<%= subjectAttendance48.ClientID %>').value;
        var kehadiran49 = document.getElementById('<%= subjectAttendance49.ClientID %>').value;
        var kehadiran50 = document.getElementById('<%= subjectAttendance50.ClientID %>').value;

        var dataKelas = [namasubjek1, namasubjek2, namasubjek3, namasubjek4, namasubjek5, namasubjek6, namasubjek7, namasubjek8, namasubjek9, namasubjek10,
            namasubjek11, namasubjek12, namasubjek13, namasubjek14, namasubjek15, namasubjek16, namasubjek17, namasubjek18, namasubjek19, namasubjek20,
            namasubjek21, namasubjek22, namasubjek23, namasubjek24, namasubjek25, namasubjek26, namasubjek27, namasubjek28, namasubjek29, namasubjek30,
            namasubjek31, namasubjek32, namasubjek33, namasubjek34, namasubjek35, namasubjek36, namasubjek37, namasubjek38, namasubjek39, namasubjek40,
            namasubjek41, namasubjek42, namasubjek43, namasubjek44, namasubjek45, namasubjek46, namasubjek47, namasubjek48, namasubjek49, namasubjek50];

        var dataKehadiran = [kehadiran1, kehadiran2, kehadiran3, kehadiran4, kehadiran5, kehadiran6, kehadiran7, kehadiran8, kehadiran9, kehadiran10,
            kehadiran11, kehadiran12, kehadiran13, kehadiran14, kehadiran15, kehadiran16, kehadiran17, kehadiran18, kehadiran19, kehadiran20,
            kehadiran21, kehadiran22, kehadiran23, kehadiran24, kehadiran25, kehadiran26, kehadiran27, kehadiran28, kehadiran29, kehadiran30,
            kehadiran31, kehadiran32, kehadiran33, kehadiran34, kehadiran35, kehadiran36, kehadiran37, kehadiran38, kehadiran39, kehadiran40,
            kehadiran41, kehadiran42, kehadiran43, kehadiran44, kehadiran45, kehadiran46, kehadiran47, kehadiran48, kehadiran49, kehadiran50];

        var barColor = ["#ff6666", "#ff8c66", "#ffb366", "#ffd966", "#ffff66", "#d9ff66", "#b3ff66", "#8cff66", "#66ff66", "#66ff8c", "#66ffb3", "#66ffd9", "#66ffff", "#66d9ff", "#66b3ff", "#668cff", "#6666ff", "#8c66ff", "#b366ff", "#d966ff", "#ff66ff", "#ff66d9", "#ff66b3", "#ff668c", "#ff6666", "#ff8c66", "#ffb366", "#ffd966", "#ffff66", "#d9ff66", "#b3ff66", "#8cff66", "#66ff66", "#66ff8c", "#66ffb3", "#66ffd9", "#66ffff", "#66d9ff", "#66b3ff", "#668cff", "#6666ff", "#8c66ff", "#b366ff", "#d966ff", "#ff66ff", "#ff66d9", "#ff66b3", "#ff668c", "#ff6666"];

        var graphData = [];
        var chartData = [];
        for (var i = 0; i < totalClass - 1; i++) {
            chartData.push({
                "country": dataKelas[i],
                "visits": dataKehadiran[i],
                "color": barColor[i]
            })

            graphData.push({
                "balloonText": "[[category]]: <b>[[value]]%</b>",
                "fontSize": 9.5,
                "labelText": "[[value]]%",
                "fillAlphas": 1,
                "title": dataKelas[i],
                "lineAlpha": 0.1,
                "type": "column",
                "valueField": "visits"
            })
        }

        var chart = AmCharts.makeChart("chartdivLPUT", {
            "theme": "light",
            "type": "serial",
            "autoMargins": false,
            "pullOutRadius": 0,
            "startDuration": 2,
            "dataProvider": chartData,
            "valueAxes": [{
                "stackType": "regular",
                "axisAlpha": 0,
                "gridAlpha": 0,
                "labelsEnabled": false,
                "position": "left"
            }],
            "graphs": [{
                "balloonText": "<b>[[category]]: [[value]]%</b>",
                "fillColorsField": "color",
                "fillAlphas": 0.9,
                "lineAlpha": 0.2,
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
                "labelsEnabled": false,
                "gridPosition": "start",
                "labelRotation": 0,
                "equalSpacing": true,
                "marginLeft": 0,
                "marginRight": 0
            }
        });
    });


</script>

<br />
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Examination</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddlSelect_Year" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_Year_OnSelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlSelect_Month" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_Month_OnSelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlSelect_Sem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_Sem_OnSelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ClientIDMode="Static" ID="ddlSelect_Class" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_Class_OnSelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ClientIDMode="Static" ID="ddlSubjectID" runat="server" Visible="true" Enabled="false" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlSelect_Subject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_Subject_SelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
        </div>
    </div>

    <br />

    <div class="container-fluid">
        <div class="row" style="text-align: center; background-color: #f2f2f2;margin-top:10px">
            <div class="col-md-12">
                <p style="background-color: #800000; display: inline-block; width: 90%; border-radius: 25px">Yearly Attendance Report</p>
                <div id="chartdivLPUO"></div>
                <br />
            </div>
        </div>
    </div>

    <br />

    <div class="container-fluid">
        <div class="row" style="text-align: center; background-color: #f2f2f2">
            <div class="col-md-12">
                <p style="background-color: #800000; display: inline-block; width: 90%; border-radius: 25px">Subject By Class</p>
                <div id="chartdivLPUT"></div>
                <br />
            </div>
        </div>
    </div>

</div>
<br />
<!-- ATTENDANCE REPORT BY YEAR -->
<asp:HiddenField ClientIDMode="static" ID="attendanceJan" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceFeb" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceMac" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceApr" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceMay" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceJun" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceJul" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceAug" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceSep" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceOct" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceNov" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="attendanceDec" runat="server" />

<!-- GET VALUE OF SUBJECT NAME, PASS TO JS -->
<asp:HiddenField ClientIDMode="static" ID="subjectName1" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName2" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName3" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName4" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName5" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName6" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName7" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName8" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName9" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName10" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName11" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName12" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName13" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName14" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName15" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName16" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName17" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName18" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName19" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName20" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName21" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName22" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName23" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName24" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName25" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName26" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName27" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName28" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName29" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName30" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName31" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName32" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName33" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName34" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName35" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName36" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName37" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName38" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName39" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName40" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName41" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName42" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName43" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName44" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName45" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName46" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName47" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName48" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName49" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectName50" runat="server" />

<!-- GET VALUE OF SUBJECT ATTENDANCE, PASS TO JS -->
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance1" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance2" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance3" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance4" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance5" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance6" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance7" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance8" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance9" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance10" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance11" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance12" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance13" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance14" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance15" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance16" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance17" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance18" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance19" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance20" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance21" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance22" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance23" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance24" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance25" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance26" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance27" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance28" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance29" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance30" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance31" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance32" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance33" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance34" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance35" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance36" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance37" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance38" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance39" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance40" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance41" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance42" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance43" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance44" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance45" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance46" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance47" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance48" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance49" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subjectAttendance50" runat="server" />



<!-- GET VALUE OF SUBJECT NAME, PASS TO JS -->
<asp:HiddenField ClientIDMode="static" ID="sub1" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub2" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub3" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub4" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub5" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub6" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub7" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub8" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub9" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub10" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub11" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub12" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub13" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub14" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub15" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub16" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub17" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub18" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub19" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub20" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub21" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub22" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub23" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub24" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub25" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub26" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub27" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub28" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub29" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="sub30" runat="server" />

<!-- GET VALUE OF SUBJECT ATTENDANCE FOR CLASS SELECTED, PASS TO JS -->
<asp:HiddenField ClientIDMode="static" ID="subAttendance1" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance2" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance3" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance4" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance5" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance6" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance7" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance8" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance9" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance10" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance11" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance12" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance13" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance14" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance15" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance16" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance17" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance18" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance19" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance20" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance21" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance22" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance23" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance24" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance25" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance26" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance27" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance28" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance29" runat="server" />
<asp:HiddenField ClientIDMode="static" ID="subAttendance30" runat="server" />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Students Attendance</p>
   
    <div style="overflow-y: scroll; overflow-x: hidden; height: 420px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="course_ID" BorderStyle="None" GridLines="None"
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

                <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="StudentID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="1000">
                    <ItemTemplate>
                        <asp:Label ID="StudentName" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Jumlah Kehadiran" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="JumlahKehadiran" class="id1" runat="server" Text='<%# Eval("JumlahHadir") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Jumlah Ketidakhadiran" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="JumlahKetidakhadiran" class="id1" runat="server" Text='<%# Eval("JumlahTidakHadir") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Peratus Kehadiran" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="PeratusKehadiran" class="id1" runat="server" Text='<%# Eval("Peratus") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
</div>

