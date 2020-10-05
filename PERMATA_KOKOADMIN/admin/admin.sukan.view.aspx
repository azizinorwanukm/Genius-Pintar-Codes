<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.sukan.view.aspx.vb" Inherits="permatapintar.admin_sukan_view" %>

<%@ Register src="../commoncontrol/sukan_view.ascx" tagname="sukan_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Sukan & Permainan>Paparan
            </td>
        </tr>
    </table>
    <uc1:sukan_view ID="sukan_view1" runat="server" />&nbsp;
</asp:Content>
