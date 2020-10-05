<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.persatuan.view.aspx.vb" Inherits="permatapintar.admin_persatuan_view" %>

<%@ Register Src="../commoncontrol/persatuan_view.ascx" TagName="persatuan_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelab & Persatuan>Paparan
            </td>
        </tr>
    </table>
    <uc1:persatuan_view ID="persatuan_view1" runat="server" />
</asp:Content>
