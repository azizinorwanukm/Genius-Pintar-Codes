<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kemaskini.maklumat.pengumuman.aspx.vb" Inherits="permatapintar.admin_kemaskini_maklumat_pengumuman" %>

<%@ Register Src="~/commoncontrol/kemaskini_maklumat_pengumuman.ascx" TagPrefix="uc1" TagName="kemaskini_maklumat_pengumuman" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kemaskini_maklumat_pengumuman runat="server" id="kemaskini_maklumat_pengumuman" />
</asp:Content>
