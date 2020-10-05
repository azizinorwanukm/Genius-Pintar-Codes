<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPeperiksaan_peperiksaan_kelas.aspx.vb" Inherits="KPP_MS.admin_laporanPeperiksaan_peperiksaan_kelas" %>

<%@ Register Src="~/commoncontrol/pengarah_laporan_peperiksaan_kelas_table.ascx" TagPrefix="uc1" TagName="pengarah_laporan_peperiksaan_kelas_table" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top:-20px">
        <uc1:pengarah_laporan_peperiksaan_kelas_table runat="server" ID="pengarah_laporan_peperiksaan_kelas_table" />
    </div>
</asp:Content>
