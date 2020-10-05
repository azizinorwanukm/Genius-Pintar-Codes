<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="ppcs.user.list.aspx.vb" Inherits="permatapintar.ppcs_user_list1" %>

<%@ Register Src="../commoncontrol/admin_ppcs_user_list.ascx" TagName="admin_ppcs_user_list"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Pengurusan Pengguna>Senarai Petugas" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:admin_ppcs_user_list ID="admin_ppcs_user_list1" runat="server" />
</asp:Content>
