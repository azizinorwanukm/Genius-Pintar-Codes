<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="esurvey.chart.aspx.vb" Inherits="PERMATA_EQTest.esurvey_chart_aspx" %>

<%@ Register Assembly="OpenFlashChart" Namespace="OpenFlashChart" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <cc1:openflashchartcontrol id="ofc_radar" runat="server" datafile="datafile/radar.aspx" height="600px" width="800px"></cc1:openflashchartcontrol>
    </div>
    <asp:LinkButton ID="lnkBack" runat="server">Back to Result Page</asp:LinkButton>

</asp:Content>
