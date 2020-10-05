<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.uniform.update.aspx.vb" Inherits="permatapintar.admin_uniform_update" %>

<%@ Register Src="../commoncontrol/uniform_update.ascx" TagName="uniform_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Badan Beruniform>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:uniform_update ID="uniform_update1" runat="server" />
</asp:Content>
