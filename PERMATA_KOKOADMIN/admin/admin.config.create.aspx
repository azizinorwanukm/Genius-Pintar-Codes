<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.config.create.aspx.vb" Inherits="permatapintar.admin_config_create" %>

<%@ Register src="../commoncontrol/config_create.ascx" tagname="config_create" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Lain-Lain>Konfigurasi>Tambah
            </td>
        </tr>
    </table>
    <uc1:config_create ID="config_create1" runat="server" />
</asp:Content>
