<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pentaksiranAkademikStudents.aspx.vb" Inherits="permatapintar.admin_pentaksiranAkademikStudents1" %>

<%@ Register Src="~/commoncontrol/admin_pentaksiranAkademikStudents.ascx" TagPrefix="uc1" TagName="admin_pentaksiranAkademikStudents" %>
<%@ Register Src="~/commoncontrol/ppcs_class_view.ascx" TagPrefix="uc1" TagName="ppcs_class_view" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_class_view runat="server" id="ppcs_class_view" />
    <uc1:admin_pentaksiranAkademikStudents runat="server" id="admin_pentaksiranAkademikStudents" />
</asp:Content>
