<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pentaksiranAkademikKelas.aspx.vb" Inherits="permatapintar.admin_pentaksiranAkademikKelas1" %>

<%@ Register Src="~/commoncontrol/admin_pentaksiranAkademikKelas.ascx" TagPrefix="uc1" TagName="admin_pentaksiranAkademikKelas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_pentaksiranAkademikKelas runat="server" id="admin_pentaksiranAkademikKelas" />
</asp:Content>
