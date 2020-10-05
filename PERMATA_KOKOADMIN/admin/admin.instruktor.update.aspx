<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.instruktor.update.aspx.vb" Inherits="permatapintar.admin_instruktor_update" %>

<%@ Register Src="../commoncontrol/instruktor_update.ascx" TagName="instruktor_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Instruktor>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:instruktor_update ID="instruktor_update1" runat="server" />
</asp:Content>
