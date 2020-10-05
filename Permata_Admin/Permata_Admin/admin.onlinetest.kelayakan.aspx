<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.onlinetest.kelayakan.aspx.vb" Inherits="permatapintar.admin_onlinetest_kelayakan" %>

<%@ Register src="commoncontrol/onlinetest_kelayakan_select.ascx" tagname="onlinetest_kelayakan_select" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="PPCS>Kelayakan Online Test" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:onlinetest_kelayakan_select ID="onlinetest_kelayakan_select1" runat="server" />
</asp:Content>
