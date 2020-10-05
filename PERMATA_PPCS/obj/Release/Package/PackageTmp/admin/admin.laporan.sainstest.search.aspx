<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.sainstest.search.aspx.vb" Inherits="permatapintar.admin_laporan_sainstest_search" %>

<%@ Register Src="../commoncontrol/sainstest_search.ascx" TagName="sainstest_search" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan Science Interest Inventory
            </td>
        </tr>
    </table>
    <uc1:sainstest_search ID="sainstest_search1" runat="server" />
    &nbsp;
</asp:Content>
