<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.config.update.aspx.vb" Inherits="permatapintar.admin_config_update" %>
<%@ Register src="../commoncontrol/config_update.ascx" tagname="config_update" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Lain-Lain>Konfigurasi
            </td>
        </tr>
    </table>
    <uc1:config_update ID="config_update1" runat="server" />
&nbsp;
</asp:Content>
