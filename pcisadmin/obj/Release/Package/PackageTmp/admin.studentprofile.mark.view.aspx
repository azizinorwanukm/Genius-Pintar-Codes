<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.mark.view.aspx.vb" Inherits="araken.pcisadmin.admin_studentprofile_mark_view" %>

<%@ Register Src="commoncontrol/userprofile_view.ascx" TagName="userprofile_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/exam_history_list.ascx" TagName="exam_history_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Profil Pelajar dan Markah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:userprofile_view ID="userprofile_view1" runat="server" />
    &nbsp;
    <uc2:exam_history_list ID="exam_history_list1" runat="server" />
</asp:Content>
