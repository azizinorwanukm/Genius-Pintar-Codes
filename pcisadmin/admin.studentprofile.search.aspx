<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.search.aspx.vb" Inherits="araken.pcisadmin.admin_studentprofile_search" %>

<%@ Register Src="commoncontrol/userprofile_search.ascx" TagName="userprofile_search" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Profil Pelajar>Carian Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:userprofile_search ID="userprofile_search1" runat="server" />
    &nbsp;
</asp:Content>
