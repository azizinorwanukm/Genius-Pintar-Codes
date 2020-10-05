<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pensyarah.update.aspx.vb" Inherits="permatapintar.admin_pensyarah_update" %>

<%@ Register Src="../commoncontrol/pensyarah_update.ascx" TagName="pensyarah_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pensyarah>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:pensyarah_update ID="pensyarah_update1" runat="server" />
</asp:Content>
