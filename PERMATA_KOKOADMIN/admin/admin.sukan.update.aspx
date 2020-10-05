<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.sukan.update.aspx.vb" Inherits="permatapintar.admin_sukan_update" %>

<%@ Register Src="../commoncontrol/sukan_update.ascx" TagName="sukan_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Sukan & Permainan>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:sukan_update ID="sukan_update1" runat="server" />
</asp:Content>
