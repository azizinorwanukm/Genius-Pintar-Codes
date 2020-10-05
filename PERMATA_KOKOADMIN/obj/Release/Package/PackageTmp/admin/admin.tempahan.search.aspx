<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.tempahan.search.aspx.vb" Inherits="permatapintar.admin_tempahan_search" %>

<%@ Register Src="../commoncontrol/tempahan_search.ascx" TagName="tempahan_search" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Carian>Tempahan
            </td>
        </tr>
    </table>
    <uc1:tempahan_search ID="tempahan_search1" runat="server" />
</asp:Content>
