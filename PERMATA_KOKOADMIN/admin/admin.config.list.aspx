<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.config.list.aspx.vb" Inherits="permatapintar.admin_config_list" %>

<%@ Register Src="../commoncontrol/config_list.ascx" TagName="config_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Lain-Lain>Senarai Konfigurasi
            </td>
        </tr>
    </table>
    <uc1:config_list ID="config_list1" runat="server" />
    &nbsp;
</asp:Content>
