<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.uniform.view.aspx.vb" Inherits="permatapintar.admin_uniform_view" %>

<%@ Register Src="../commoncontrol/uniform_view.ascx" TagName="uniform_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Badan Beruniform>Paparan
            </td>
        </tr>
    </table>
    <uc2:uniform_view ID="uniform_view1" runat="server" />
</asp:Content>
