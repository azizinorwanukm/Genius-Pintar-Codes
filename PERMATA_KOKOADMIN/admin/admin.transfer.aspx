<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.transfer.aspx.vb" Inherits="permatapintar.admin_transfer" %>

<%@ Register Src="../commoncontrol/ukm3_transfer.ascx" TagName="ukm3_transfer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>
                Lain-Lain>Pelajar Baru
            </td>
        </tr>
    </table>
    <uc1:ukm3_transfer ID="ukm3_transfer1" runat="server" />
    &nbsp;
</asp:Content>
