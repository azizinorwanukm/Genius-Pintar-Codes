<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPeperiksaan_kedudukan_pelajar.aspx.vb" Inherits="KPP_MS.admin_laporanPeperiksaan_kedudukan_pelajar" %>

<%@ Register Src="~/commoncontrol/pengarah_laporan_peperiksaan_table.ascx" TagPrefix="uc1" TagName="pengarah_laporan_peperiksaan_table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc1:pengarah_laporan_peperiksaan_table runat="server" ID="pengarah_laporan_peperiksaan_table" />
</asp:Content>
