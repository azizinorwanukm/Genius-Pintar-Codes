<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.view.aspx.vb" Inherits="araken.pcisadmin.admin_studentprofile_view" %>

<%@ Register Src="commoncontrol/userprofile_view.ascx" TagName="userprofile_view" TagPrefix="uc1" %>
<%@ Register src="commoncontrol/exam_history_list.ascx" tagname="exam_history_list" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Profil Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:userprofile_view ID="userprofile_view1" runat="server" />
    &nbsp;
    <uc2:exam_history_list ID="exam_history_list1" runat="server" />
</asp:Content>
