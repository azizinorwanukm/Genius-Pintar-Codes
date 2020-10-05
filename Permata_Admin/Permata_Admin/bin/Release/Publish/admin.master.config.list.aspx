<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.master.config.list.aspx.vb" Inherits="permatapintar.admin_master_config_list" %>

<%@ Register Src="commoncontrol/master_config_list.ascx" TagName="master_config_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Lain-lain>Sistem Konfigurasi>Senarai
            </td>
        </tr>
    </table>
    <uc2:master_config_list ID="master_config_list1" runat="server" />
</asp:Content>
