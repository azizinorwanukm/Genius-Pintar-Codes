<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penjaga.Master" CodeBehind="penjaga_laporan_peperiksaan.aspx.vb" Inherits="KPP_SYS.penjaga_laporan_peperiksaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>

    <script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
    <script src="https://www.amcharts.com/lib/3/serial.js"></script>
    <script src="https://www.amcharts.com/lib/3/plugins/export/export.min.js"></script>
    <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />

    <script>
        function printExam() {
            var divToPrint = document.getElementById("keputusan");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }

        $(document).ready(function () {
            var A_plus = document.getElementById('<%= HiddenFieldA_plus.ClientID %>').value;
            var A = document.getElementById('<%= HiddenFieldA.ClientID %>').value;
            var A_minus = document.getElementById('<%= HiddenFieldA_minus.ClientID %>').value;
            var B_plus = document.getElementById('<%= HiddenFieldB_plus.ClientID %>').value;
            var B = document.getElementById('<%= HiddenFieldB.ClientID %>').value;
            var B_minus = document.getElementById('<%= HiddenFieldB_minus.ClientID %>').value;
            var C_plus = document.getElementById('<%= HiddenFieldC_plus.ClientID %>').value;
            var C = document.getElementById('<%= HiddenFieldC.ClientID %>').value;
            var D = document.getElementById('<%= HiddenFieldD.ClientID %>').value;
            var E = document.getElementById('<%= HiddenFieldE.ClientID %>').value;
            var G = document.getElementById('<%= HiddenFieldG.ClientID %>').value;

            var chart = AmCharts.makeChart("chartdivP_LP", {
                "theme": "light",
                "type": "serial",
                "startDuration": 2,
                "dataProvider": [{
                    "country": "A+",
                    "visits": A_plus,
                    "color": "#2A0CD0"
                }, {
                    "country": "A",
                    "visits": A,
                    "color": "#0D52D1"
                }, {
                    "country": "A-",
                    "visits": A_minus,
                    "color": "#0D8ECF"
                }, {
                    "country": "B+",
                    "visits": B_plus,
                    "color": "#04D215"
                }, {
                    "country": "B",
                    "visits": B,
                    "color": "#B0DE09"
                }, {
                    "country": "B- ",
                    "visits": B_minus,
                    "color": "#F8FF01"
                }, {
                    "country": "C+",
                    "visits": C_plus,
                    "color": "#FCD202"
                }, {
                    "country": "C",
                    "visits": C,
                    "color": "#FF9E01"
                }, {
                    "country": "D",
                    "visits": D,
                    "color": "#FF6600"
                }, {
                    "country": "E",
                    "visits": E,
                    "color": "#FF0F00"
                }, {
                    "country": "G",
                    "visits": G,
                    "color": "#000000"
                }],
                "graphs": [{
                    "balloonText": "[[category]]: <b>[[value]]</b>",
                    "fillColorsField": "color",
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
                    "labelRotation": 90
                },
                "export": {
                    "enabled": true
                }
            });
        });
    </script>

    <br />
    <div class="container-fluid">
        <div class="row" style="text-align: center; background-color: lightblue">
            <div class="col-md-6">
                <p style="background-color: royalblue">Laporan Prestasi Pelajar</p>
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldA_plus" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldA" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldA_minus" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldB_plus" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldB" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldB_minus" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldC_plus" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldC" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldD" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldE" runat="server" />
                <asp:HiddenField ClientIDMode="static" ID="HiddenFieldG" runat="server" />
                <div id="chartdivP_LP"></div>
                <br />
            </div>
            <div class="col-md-6">
                <p style="background-color: royalblue">Laporan Grade Pelajar</p>
                <table style="width: 100%; height: 150px; background-color: #FFFFFF" class="w3-border w3-text-black">
                    <tr>
                        <td style="width: 100%" class="w3-border" colspan="2">UJIAN 1</td>
                    </tr>
                    <tr>
                        <td style="width: 50%" class="w3-border">GPA</td>
                        <td style="width: 50%" class="w3-border">3.65</td>
                    </tr>
                    <tr>
                        <td style="width: 50%" class="w3-border">CGPA</td>
                        <td style="width: 50%" class="w3-border">3.65</td>
                    </tr>
                    <tr>
                        <td style="width: 50%" class="w3-border">Kedudukan Dalam Kelas</td>
                        <td style="width: 50%" class="w3-border">10/25</td>
                    </tr>
                    <tr>
                        <td style="width: 50%" class="w3-border">Kedudukan Kesuluruhan</td>
                        <td style="width: 50%" class="w3-border">50/200</td>
                    </tr>
                    <tr>
                        <td style="width: 50%" class="w3-border">Kehadiran</td>
                        <td style="width: 50%" class="w3-border">123/125</td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
    <br />

    <div style="width: 100%; background-color: lightblue; text-align: center">
        <p style="background-color: royalblue">Laporan Markah Ujian Pelajar</p>
        <asp:PlaceHolder ID="pelajar_exam" runat="server"></asp:PlaceHolder>
        <br />
    </div>
    <br />
    <div style="text-align: center">
        <button type="button" class="btn btn-info" style="background-color: #005580" onclick="printExam()">Cetak</button>
    </div>
</asp:Content>
