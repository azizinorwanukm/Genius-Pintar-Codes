<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah.Master" CodeBehind="pengarah_laporan_kursus.aspx.vb" Inherits="KPP_MS.pengarah_laporan_kursus" %>

<%@ Register Src="~/commoncontrol/pengarah_laporan_peperiksaan_kursus_table.ascx" TagPrefix="uc1" TagName="pengarah_laporan_peperiksaan_kursus_table" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pengarah_laporan_peperiksaan_kursus_table runat="server" id="pengarah_laporan_peperiksaan_kursus_table" />
</asp:Content>
