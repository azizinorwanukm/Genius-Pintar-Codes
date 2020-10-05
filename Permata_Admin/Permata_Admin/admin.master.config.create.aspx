<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.master.config.create.aspx.vb" Inherits="permatapintar.admin_master_config_create" %>

<%@ Register Src="commoncontrol/master_config_create.ascx" TagName="master_config_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Lain-lain>Sistem Konfigurasi>Tambah
            </td>
        </tr>
    </table>
    <uc1:master_config_create ID="master_config_create1" runat="server" />
</asp:Content>
