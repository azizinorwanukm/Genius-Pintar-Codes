<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.stresstest.search.aspx.vb" Inherits="permatapintar.admin_laporan_stresstest_search" %>

<%@ Register Src="../commoncontrol/stresstest_search.ascx" TagName="stresstest_search" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan Stress Test
            </td>
        </tr>
    </table>
    <uc1:stresstest_search ID="stresstest_search1" runat="server" />
    &nbsp;
</asp:Content>
