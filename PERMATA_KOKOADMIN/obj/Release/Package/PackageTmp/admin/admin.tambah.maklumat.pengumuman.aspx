<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.tambah.maklumat.pengumuman.aspx.vb" Inherits="permatapintar.admin_tambah_maklumat_pengumuman" %>

<%@ Register Src="~/commoncontrol/tambah_maklumat_pengumuman.ascx" TagPrefix="uc1" TagName="tambah_maklumat_pengumuman" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:tambah_maklumat_pengumuman runat="server" id="tambah_maklumat_pengumuman" />
</asp:Content>
