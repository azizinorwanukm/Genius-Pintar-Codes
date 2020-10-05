<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.rumahsukan.update.aspx.vb" Inherits="permatapintar.admin_rumahsukan_update" %>

<%@ Register Src="../commoncontrol/rumahsukan_update.ascx" TagName="rumahsukan_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Rumah Sukan>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:rumahsukan_update ID="rumahsukan_update1" runat="server" />
</asp:Content>
