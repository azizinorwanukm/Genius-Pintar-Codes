<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.rumahsukan.view.aspx.vb" Inherits="permatapintar.admin_rumahsukan_view" %>

<%@ Register src="../commoncontrol/rumahsukan_view.ascx" tagname="rumahsukan_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Rumah Sukan>Paparan
            </td>
        </tr>
    </table>
    <uc1:rumahsukan_view ID="rumahsukan_view1" runat="server" />&nbsp;
</asp:Content>
