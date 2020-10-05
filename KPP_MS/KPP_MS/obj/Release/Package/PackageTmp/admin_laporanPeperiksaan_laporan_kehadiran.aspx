<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPeperiksaan_laporan_kehadiran.aspx.vb" Inherits="KPP_MS.admin_laporanPeperiksaan_laporan_kehadiran" %>

<%@ Register Src="~/commoncontrol/pengarah_laporan_kehadiran_table.ascx" TagPrefix="uc1" TagName="pengarah_laporan_kehadiran_table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top:-20px">
        <uc1:pengarah_laporan_kehadiran_table runat="server" ID="pengarah_laporan_kehadiran_table" />
    </div>
</asp:Content>
