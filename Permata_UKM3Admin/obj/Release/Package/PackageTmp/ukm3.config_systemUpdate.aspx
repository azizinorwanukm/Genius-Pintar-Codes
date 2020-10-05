<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3.config_systemUpdate.aspx.vb" Inherits="permatapintar.ukm3_config_systemUpdate" %>

<%@ Register Src="~/commoncontrol/master_config_update.ascx" TagPrefix="uc1" TagName="master_config_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table class="fbform">
        <tr class="fbform_header">
            <td>Lain-lain>Sistem Konfigurasi>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:master_config_update runat="server" ID="master_config_update" />
</asp:Content>
