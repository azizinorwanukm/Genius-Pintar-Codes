<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pentaksiranAkademik.aspx.vb" Inherits="permatapintar.admin_pentaksiranAkademik" %>

<%@ Register Src="~/commoncontrol/admin_pentaksiran_akademik.ascx" TagPrefix="uc1" TagName="admin_pentaksiran_akademik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_pentaksiran_akademik runat="server" id="admin_pentaksiran_akademik" />
</asp:Content>

