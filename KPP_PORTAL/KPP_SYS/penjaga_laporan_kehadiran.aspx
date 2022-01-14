<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penjaga.Master" CodeBehind="penjaga_laporan_kehadiran.aspx.vb" Inherits="KPP_SYS.penjaga_laporan_kehadiran" %>

<%@ Register Src="~/commoncontrol/parent_attendanceData.ascx" TagPrefix="uc1" TagName="parent_attendanceData" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   

    <uc1:parent_attendanceData runat="server" id="parent_attendanceData" />

</asp:Content>
