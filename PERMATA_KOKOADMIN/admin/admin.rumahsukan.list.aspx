<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.rumahsukan.list.aspx.vb" Inherits="permatapintar.admin_rumahsukan_list" %>

<%@ Register Src="../commoncontrol/rumahsukan_list.ascx" TagName="rumahsukan_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Rumah Sukan
            </td>
        </tr>
    </table>
    <uc1:rumahsukan_list ID="sukan_list1" runat="server" />
</asp:Content>