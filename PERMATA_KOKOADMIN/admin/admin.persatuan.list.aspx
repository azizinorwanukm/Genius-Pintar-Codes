<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.persatuan.list.aspx.vb" Inherits="permatapintar.admin_persatuan_list" %>
<%@ Register src="../commoncontrol/persatuan_list.ascx" tagname="persatuan_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelab & Persatuan
            </td>
        </tr>
    </table>
    <uc1:persatuan_list ID="persatuan_list1" runat="server" />

</asp:Content>
