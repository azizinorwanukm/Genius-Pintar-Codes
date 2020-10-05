<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.eqtest.search.aspx.vb" Inherits="permatapintar.admin_laporan_eqtest_search" %>
<%@ Register src="../commoncontrol/eqtest_search.ascx" tagname="eqtest_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan EQTest
            </td>
        </tr>
    </table>
    <uc1:eqtest_search ID="eqtest_search1" runat="server" />
&nbsp;
</asp:Content>
