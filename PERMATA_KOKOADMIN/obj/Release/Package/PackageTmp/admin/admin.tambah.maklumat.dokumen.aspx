<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.tambah.maklumat.dokumen.aspx.vb" Inherits="permatapintar.admin_tambah_maklumat_dokumen" %>

<%@ Register Src="~/commoncontrol/tambah_maklumat_dokumen.ascx" TagPrefix="uc1" TagName="tambah_maklumat_dokumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:tambah_maklumat_dokumen runat="server" id="tambah_maklumat_dokumen" />
</asp:Content>
