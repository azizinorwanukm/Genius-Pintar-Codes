<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.userprofile.list.admin.aspx.vb" Inherits="permatapintar.admin_userprofile_list_admin" %>

<%@ Register Src="commoncontrol/userprofile_list_admin.ascx" TagName="userprofile_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Lain-lain>Senarai Pengguna Sistem" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:userprofile_list ID="userprofile_list1" runat="server" />
</asp:Content>
