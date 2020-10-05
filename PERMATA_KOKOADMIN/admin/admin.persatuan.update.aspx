<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.persatuan.update.aspx.vb" Inherits="permatapintar.admin_persatuan_update" %>

<%@ Register Src="../commoncontrol/persatuan_update.ascx" TagName="persatuan_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelab & Persatuan>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:persatuan_update ID="persatuan_update1" runat="server" />
</asp:Content>
